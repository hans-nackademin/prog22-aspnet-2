using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Helpers.Repositories;

namespace WebApi.Tests.UnitTests.Repositories
{
    public class ProductRepositoryTests
    {
        private DataContext _context;
        private IProductRepository _productRepo;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _productRepo = new ProductRepository(_context);
        }

        [Fact]
        public async Task AddAsync_AddProductToDatabase_ReturnCreatedProduct()
        {
            var entity = ProductFixture.Entities.First();

            var result = await _productRepo.AddAsync(entity);

            Assert.NotNull(result);
            var data = Assert.IsType<ProductEntity>(result);
            Assert.Equal(entity.Id, data.Id);
        }


        [Fact]
        public async Task GetAsync_GetOneProductFromDatabase_ReturnProduct()
        {
            var entity = ProductFixture.Entities.First();
            await _productRepo.AddAsync(entity);

            var result = await _productRepo.GetAsync(x => x.Id == entity.Id);

            Assert.NotNull(result);
            var data = Assert.IsType<ProductEntity>(result);
            Assert.Equal(entity.Id, data.Id);
        }

        [Fact]
        public async Task GetAsync_TryToOneProductFromDatabase_ReturnNull()
        {
            var entity = ProductFixture.Entities.First();

            var result = await _productRepo.GetAsync(x => x.Id == entity.Id);

            Assert.Null(result);

        }

        [Fact]
        public async Task GetAllAsync_GetAllProductsFromDatabase_ReturnProducts()
        {
            foreach(var entity in ProductFixture.Entities)
                await _productRepo.AddAsync(entity);

            var result = await _productRepo.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result);
            Assert.Equal(ProductFixture.Entities.Count, result.Count());
        }


        [Fact]
        public async Task AnyAsync_CheckIfProductExists_ReturnTrueIfProductExists()
        {
            var entity = ProductFixture.Entities.First();
            await _productRepo.AddAsync(entity);

            var result = await _productRepo.AnyAsync(x => x.Id == entity.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task AnyAsync_CheckIfProductExists_ReturnTrueIfProductNotExists()
        {
            var entity = ProductFixture.Entities.First();

            var result = await _productRepo.AnyAsync(x => x.Id == entity.Id);

            Assert.False(result);
        }
    }
}
