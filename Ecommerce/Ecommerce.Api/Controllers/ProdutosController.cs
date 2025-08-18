using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        public ProdutosController() { }

        [HttpGet]
        public OkObjectResult Produtos()
        {
            return Ok(new object[]
            {
                new
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Teste 1",
                    Preco = 12.50M
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Teste 2",
                    Preco = 28.99M
                },
            });
        }
    }
}
