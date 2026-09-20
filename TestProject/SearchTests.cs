using Moq;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;
using Searchify.Infrastructure.Services;

namespace Searchify.Test;

public class SearchServiceTests
{
    [Fact]
    public async Task SearchAsync_SavesHistoryAndReturnsPagedResult()
    {
        var repo = new Mock<IProductRepository>();
        repo.Setup(x => x.SearchAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new List<Product>
            {
                new() { ProductId = 1, Name = "Laptop", Price = 899 }
            }, 1));

        var service = new ProductService(repo.Object);
        var result = await service.SearchAsync(new SearchRequest { Query = "laptop", Page = 1, PageSize = 10 }, "admin");

        Assert.Single(result.Items);
        Assert.Equal(1, result.TotalCount);
        repo.Verify(x => x.SaveSearchHistoryAsync(
            It.Is<SearchHistory>(h => h.UserName == "admin" && h.Query == "laptop" && h.ResultCount == 1),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_RejectsInvalidPriceRange()
    {
        var repo = new Mock<IProductRepository>();
        var service = new ProductService(repo.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SearchAsync(new SearchRequest { MinPrice = 100, MaxPrice = 50 }, "admin"));
    }
}
