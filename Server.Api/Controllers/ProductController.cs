using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.DTOs.RequestDTOs;
using Server.Domain.Interfaces;

namespace Server.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromForm] AddProductDTO model)
        {
            await productService.Add(model);
            return Ok();
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromForm] UpdateProductDTO model)
        {
            await productService.Update(model);
            return Ok();
        }

        [HttpPost("getSimpleList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSimpleList([FromBody] ProductRequestDTO model)
        {
            return Ok(await productService.GetSimpleList(model));
        }

        [HttpPut("updateStatus")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateProductStatus model)
        {
            await productService.UpdateStatus(model);
            return Ok();
        }
    }
}
