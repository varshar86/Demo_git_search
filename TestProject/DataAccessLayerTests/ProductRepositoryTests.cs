using Microsoft.EntityFrameworkCore;
using Searchify.Domain.Model;
using Searchify.Infrastructure;
using Searchify.Infrastructure.Repositories;

namespace Searchify.Test.DataAccessLayerTests
{
    public class ProductsRepositoryTests
    {
        private DbContextOptions<AppDbContext> GetInMemoryDbOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Products.AddRange(
                    new Product { ProductId = 1, Name = "Product1", Price = 100 },
                    new Product { ProductId = 2, Name = "Product2", Price = 200 }
                );
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new ProductRepository(context);

                // Act
                var result = await repository.GetAllProducts();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct_WhenIdExists()
        {
            // Arrange
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Products.Add(new Product { ProductId = 3, Name = "Product1", Price = 100 });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new ProductRepository(context);

                // Act
                var result = await repository.GetProductById(3);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Product1", result.Name);
            }
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            // Arrange
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Products.Add(new Product { ProductId = 4, Name = "Product1", Price = 100 });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new ProductRepository(context);

                // Act
                var result = await repository.GetProductById(5);

                // Assert
                Assert.Null(result);
            }
        }
    }
}


