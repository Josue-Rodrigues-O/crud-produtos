using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        [HttpPost]
        public CreatedResult Create([FromBody] ProductDto productDto)
        {
            var product = productService.Create(productDto);
            return Created(product.Id.ToString(), product);
        }

        [HttpGet]
        public OkObjectResult GetAll([FromQuery] ProductFilter filter)
        {
            return Ok(productService.GetAll(filter));
        }

        [HttpGet("{id:guid}")]
        public OkObjectResult GetById([FromRoute] Guid id)
        {
            return Ok(productService.GetById(id));
        }

        [HttpPut("{id:guid}")]
        public NoContentResult Update([FromRoute] Guid id, ProductDto productDto)
        {
            productService.Update(id, productDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public NoContentResult Delete([FromRoute] Guid id)
        {
            productService.Delete(id);
            return NoContent();
        }
    }
}
