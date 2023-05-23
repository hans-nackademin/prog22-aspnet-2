using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Services;
using WebApi.Models.Schemas;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    #region constructor

    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    #endregion

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_productService.GetAll());      
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var item = _productService.Get(x => x.Id == id);
        if (item != null)            
            return Ok(item);

        return NotFound();
    }

    [HttpPost]
    public IActionResult Post(ProductSchema schema)
    {
        if (ModelState.IsValid)
        {
            var item = _productService.Get(x => x.Name == schema.Name);
            if (item != null)
                return Conflict(item);

            return Ok(_productService.Add(schema));
        }

        return BadRequest();
    }
}
