using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs.CustomerDTOs;
using Server.Domain.Interfaces;

namespace Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountSevice;
        public AccountController(IAccountService accountSevice)
        {
            this.accountSevice = accountSevice;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDTO model)
        {
            return Ok(await accountSevice.Login(model));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CustomerCreateDTO model)
        {
            await accountSevice.Register(model);
            return Ok();
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CustomerCreateDTO model)
        {
            await accountSevice.RegisterAdmin(model);
            return Ok();
        }

        [HttpPost]
        [Route("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDTO externalAuth)
        {
            return Ok(await accountSevice.ExternalLogin(externalAuth));
        }
    }
}
