using NuGet.Frameworks;
using System.Linq.Expressions;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;

namespace WebApi.Tests.UnitTests.Services
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepo;
        private IProductService _productService;


        public ProductServiceTests()
        {
            _productRepo = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepo.Object);
        }


        [Fact]
        public async Task CreateAsync_CreateNewProduct_ReturnCreatedProduct()
        {
            var schema = ProductFixture.Schemas[4];
            var entity = ProductFixture.Entities[4];
            var product = ProductFixture.Products[4];
            _productRepo.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync(false);
            _productRepo.Setup(x => x.AddAsync(It.IsAny<ProductEntity>())).ReturnsAsync(entity);

            var result = await _productService.CreateAsync(schema);

            var data = Assert.IsType<Product>(result);
            Assert.Equal(product.Name, data.Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = ProductFixture.Entities;
            _productRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllAsync();

            // Assert
            Assert.Equal(products.Count, result.Count());
            _productRepo.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ReturnsExpectedProduct()
        {
            // Arrange
            var product = ProductFixture.Entities.First();
            _productRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync(product);

            // Act
            var result = await _productService.GetAsync(x => x.Name == product.Name);

            // Assert
            Assert.Equal(product.Name, result.Name);
            _productRepo.Verify(x => x.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task AnyAsync_ReturnsExpectedResult()
        {
            // Arrange
            var product = ProductFixture.Entities.First();
            _productRepo.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync(true);

            // Act
            var result = await _productService.AnyAsync(x => x.Name == product.Name);

            // Assert
            Assert.True(result);
            _productRepo.Verify(x => x.AnyAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()), Times.Once);
        }
    }
}
