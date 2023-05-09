using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs.CategoryDTOs;
using Server.Domain.Interfaces;

namespace Server.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService languageService;

        public LanguageController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Add()
        {
            return Ok(await languageService.GetAll());
        }
    }
}
