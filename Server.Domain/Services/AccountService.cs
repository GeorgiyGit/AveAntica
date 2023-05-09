using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Domain.DTOs.CustomerDTOs;
using Server.Domain.Entities;
using Server.Domain.Exceptions;
using Server.Domain.Help_Elements;
using Server.Domain.Interfaces;
using Server.Domain.Resources;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IConfigurationSection _googleSettings;
        public AccountService(UserManager<Customer> userManager,
                              RoleManager<IdentityRole> _roleManager,
                              IMapper mapper,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this._roleManager = _roleManager;
            this.configuration = configuration;

            _googleSettings = configuration.GetSection("GoogleAuthSettings");
        }

        public async Task<TokenDTO> Login(CustomerLoginDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                return new TokenDTO()
                {
                    Token = await CreateTokenAsync(user)
                };
            }
            throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);
        }


        public async Task Register(CustomerCreateDTO model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                throw new HttpException(ErrorMessages.UserExists, HttpStatusCode.NotFound);

            Customer user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            //if (model.Avatar != null) user.Avatar = mapper.Map<Image>(model.Avatar);

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new HttpException(ErrorMessages.UserCreationFailed, HttpStatusCode.InternalServerError);

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

        }

        public async Task RegisterAdmin(CustomerCreateDTO model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                throw new HttpException(ErrorMessages.UserExists, HttpStatusCode.NotFound);

            Customer user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new HttpException(ErrorMessages.UserCreationFailed, HttpStatusCode.InternalServerError);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }
        public async Task<string> CreateTokenAsync(Customer user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = configuration.GetSection("JwtOptions");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(Customer user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("JwtOptions");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Lifetime"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDTO externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleSettings.GetSection("clientId").Value }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);

                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }

        public async Task<TokenDTO> ExternalLogin(ExternalAuthDTO externalAuth)
        {
            var payload = await VerifyGoogleToken(externalAuth);
            if (payload == null)
                throw new HttpException(ErrorMessages.ExternalAuth, HttpStatusCode.BadRequest);

            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new Customer { Email = payload.Email, UserName = payload.Email };
                    await userManager.CreateAsync(user);
                    //prepare and send an email for the email confirmation
                    if (await _roleManager.RoleExistsAsync(UserRoles.User))
                        await userManager.AddToRoleAsync(user, UserRoles.User);

                    await userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                throw new HttpException(ErrorMessages.ExternalAuth, HttpStatusCode.BadRequest);

            var token = await CreateTokenAsync(user);
            return new TokenDTO
            {
                Token = token,
            };
        }
    }
}
