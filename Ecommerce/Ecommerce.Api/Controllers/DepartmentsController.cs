using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentsController(DepartmentService departmentService) : ControllerBase
    {
        [HttpGet]
        public OkObjectResult GetAll()
        {
            return Ok(departmentService.GetAll());
        }

        [HttpGet("{code}")]
        public OkObjectResult GetById([FromRoute] string code)
        {
            return Ok(departmentService.GetByCode(code));
        }
    }
}
