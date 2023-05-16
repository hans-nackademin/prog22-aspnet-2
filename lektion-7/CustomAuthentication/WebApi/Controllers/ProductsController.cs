using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(new List<string>()
            {
                "product 1",
                "product 2"
            });
        }
    }
}
