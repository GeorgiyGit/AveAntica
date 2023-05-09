using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs.CategoryDTOs;
using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.Interfaces;

namespace Server.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromForm] AddCategoryDTO model)
        {
            await categoryService.Add(model);
            return Ok();
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDTO model)
        {
            await categoryService.Update(model);
            return Ok();
        }

        [HttpPost("simple")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSimpleCategories(GetSimpleCategoriesDTO requestDTO)
        {
            return Ok(await categoryService.GetSimpleCategories(requestDTO));
        }

        [HttpPost("normal")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories(GetSimpleCategoriesDTO requestDTO)
        {
            return Ok(await categoryService.GeetCategories(requestDTO));
        }
    }
}
