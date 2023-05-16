using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Services;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager _userManager;

        public AuthenticationController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterSchema schema)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.CreateUserAsync(schema, schema.Password))
                    return Ok();
            }

            return BadRequest();
        }
    }
}
