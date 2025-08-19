using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] UserDto req)
        {
            var tokenString = authService.Login(req.Username, req.Password);

            if (string.IsNullOrEmpty(tokenString))
                return Unauthorized();

            return Ok(new { access_token = tokenString });
        }
    }
}
