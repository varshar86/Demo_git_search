using Moq;
using Searchify.Application.Queries;
using Searchify.Application.CommandHandlers;
using Searchify.Application.Commands;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;

namespace Searchify.Test.CommandHandlerTests
{
    public class GetAllProductsCommandHandlerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly GetAllProductsCommandHandler _handler;

        public GetAllProductsCommandHandlerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _handler = new GetAllProductsCommandHandler(_mockProductService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1" },
                new Product { ProductId = 2, Name = "Product2" }
            };

            _mockProductService.Setup(service => service.GetAllProducts())
                               .ReturnsAsync(products);

            var command = new GetAllProductsQuery();
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            var productList = Assert.IsType<List<Product>>(result);
            Assert.Equal(2, productList.Count);
            Assert.Contains(productList, p => p.Name == "Product1");
            Assert.Contains(productList, p => p.Name == "Product2");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetAllProducts())
                               .ReturnsAsync(new List<Product>());

            var command = new GetAllProductsQuery();
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            var productList = Assert.IsType<List<Product>>(result);
            Assert.Empty(productList);
        }
    }
}
