using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Services;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    [Route("api/authentication")]
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
                    return Created("", null);
            }

            return BadRequest();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginSchema schema)
        {
            if (ModelState.IsValid)
            {
                var token = await _userManager.LoginAsync(schema.Email, schema.Password);
                if (token != null)
                    return Ok(token);

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
