using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController(ProductService productService) : ControllerBase
    {
        [HttpPost]
        public CreatedResult Create([FromBody] ProductDto productDto)
        {
            var product = productService.Create(productDto);
            return Created(product.Id.ToString(), product);
        }

        [HttpGet]
        public OkObjectResult GetAll()
        {
            return Ok(productService.GetAll());
        }

        [HttpGet("{id:guid}")]
        public OkObjectResult GetById([FromRoute] Guid id)
        {
            return Ok(productService.GetById(id));
        }

        [HttpPut("{id:guid}")]
        public void Update([FromRoute] Guid id, ProductDto productDto)
        {
            productService.Update(id, productDto);
        }

        [HttpDelete("{id:guid}")]
        public NoContentResult Delete([FromRoute] Guid id)
        {
            productService.Delete(id);
            return NoContent();
        }
    }
}
