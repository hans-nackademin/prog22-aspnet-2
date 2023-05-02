using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Repositories;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepo;

        public ProductsController(ProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductSchema schema)
        {
            if (ModelState.IsValid)
                return Ok(await _productRepo.AddAsync(schema));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepo.GetAllAsync());
        }
    }
}
