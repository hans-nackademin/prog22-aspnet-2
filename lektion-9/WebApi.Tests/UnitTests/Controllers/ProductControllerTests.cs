using Moq;
using System.Linq.Expressions;
using WebApi.Controllers;

namespace WebApi.Tests.UnitTests.Controllers
{
    public class ProductControllerTests
    {
        private Mock<IProductService> _productService;
        private ProductController _controller;

        public ProductControllerTests()
        {
            _productService = new Mock<IProductService>();
            _controller = new ProductController(_productService.Object);
        }

        [Fact]
        public async Task Create_ShouldCreateAlreadyExistingProduct_ReturnConflictWithMessage()
        {
            // arrange
            var schema = ProductFixture.Schemas[0];
            var product = ProductFixture.Products[0];

            _productService.Setup(x => x.AnyAsync(x => x.Name == schema.Name)).ReturnsAsync(true);

            // act
            var result = await _controller.Create(schema);

            // assert
            Assert.NotNull(result);
            var response = Assert.IsType<ConflictObjectResult>(result);
            var value = Assert.IsType<string>(response.Value);
            Assert.Equal(ProductFixture.ErrorMessages[0], value);
        }


        [Fact]
        public async Task Create_ShouldCreateProduct_ReturnCreatedWithProduct()
        {
            // arrange
            var schema = ProductFixture.Schemas[0];
            var product = ProductFixture.Products[0];
            _productService.Setup(x => x.CreateAsync(schema)).ReturnsAsync(product);

            // act
            var result = await _controller.Create(schema);

            // assert
            Assert.NotNull(result);
            var response = Assert.IsType<CreatedResult>(result);
            var value = Assert.IsType<Product>(response.Value);
            Assert.Equal(product, value);
        }



        [Fact]
        public async Task Create_ReturnsCreatedWhenProductDoesNotExist()
        {
            // Arrange
            var schema = ProductFixture.Schemas.First();
            var product = ProductFixture.Products.First();
            _productService.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync(false);
            _productService.Setup(x => x.CreateAsync(schema)).ReturnsAsync(product);

            // Act
            var result = await _controller.Create(schema);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsBadRequestWhenIdIsInvalid()
        {
            // Act
            var result = await _controller.Get(0);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundWhenProductDoesNotExist()
        {
            // Arrange
            _productService.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsOkWhenProductExists()
        {
            // Arrange
            var product = ProductFixture.Products.First();
            _productService.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync(product);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFoundWhenNoProductsExist()
        {
            // Arrange
            _productService.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWhenProductsExist()
        {
            // Arrange
            var products = ProductFixture.Products;
            _productService.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
