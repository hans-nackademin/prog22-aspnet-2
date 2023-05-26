using System.Net;
using System.Net.Http.Json;

namespace WebApi.Tests.IntergrationTests.Controllers
{
    public class ProductControllerIntegrationTests : IDisposable
    {
        private Mock<IProductService> _productService;
        private ProductController _controller;
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public ProductControllerIntegrationTests()
        {
            _productService = new Mock<IProductService>();
            _controller = new ProductController(_productService.Object);
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }


        [Fact]
        public async Task Create_ShouldWhenModelStateIsNotValid_ReturnBadRequest()
        {
            // arrange
            var schema = new ProductSchema();

            // act
            var result = await _client.PostAsync("/api/products", JsonContent.Create(schema));

            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

    }
}
