using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs.CategoryDTOs;
using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.DTOs.TagDTOs;
using Server.Domain.Interfaces;

namespace Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController: ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddTagDTO model)
        {
            await tagService.Add(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateTagDTO model)
        {
            await tagService.Update(model);
            return Ok();
        }

        [HttpGet("simple/{languageCode}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> GetSimpleTags(string languageCode)
        {
            return Ok(await tagService.GetSimpleTags(languageCode));
        }
        [HttpGet("simple2/{languageCode}")]
        [Authorize]

        public async Task<IActionResult> GetSimpleTagsTest(string languageCode)
        {
            return Ok(await tagService.GetSimpleTags(languageCode));
        }

        [HttpGet("simple3")]
        [Authorize]

        public async Task<IActionResult> GetSimpleTagsTest2()
        {
            return Ok(await tagService.GetSimpleTags("ukr"));
        }
    }
}
