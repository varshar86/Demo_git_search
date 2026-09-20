using Moq;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;
using Searchify.Infrastructure.Services;

namespace Searchify.Test.ServiceTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1" },
                new Product { ProductId = 2, Name = "Product2" }
            };

            _mockProductRepository.Setup(repo => repo.GetAllProducts())
                                  .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            var productList = Assert.IsType<List<Product>>(result);
            Assert.Equal(2, productList.Count);
            Assert.Contains(productList, p => p.Name == "Product1");
            Assert.Contains(productList, p => p.Name == "Product2");
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetAllProducts())
                                  .ReturnsAsync(new List<Product>());

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            var productList = Assert.IsType<List<Product>>(result);
            Assert.Empty(productList);
        }

        [Fact]
        public async Task GetAllProducts_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetAllProducts())
                                  .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _productService.GetAllProducts());
            Assert.Equal("Database error", exception.Message);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product1" };

            _mockProductRepository.Setup(repo => repo.GetProductById(1))
                                  .ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductById(1);

            // Assert
            var returnedProduct = Assert.IsType<Product>(result);
            Assert.Equal(1, returnedProduct.ProductId);
            Assert.Equal("Product1", returnedProduct.Name);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetProductById(It.IsAny<int>()))
                                  .ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductById(999); // Invalid ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProductById_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetProductById(1))
                                  .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _productService.GetProductById(1));
            Assert.Equal("Database error", exception.Message);
        }
    }
}

