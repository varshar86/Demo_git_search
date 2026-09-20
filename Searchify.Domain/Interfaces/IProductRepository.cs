using Searchify.Domain.Model;

namespace Searchify.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task<(IReadOnlyList<Product> Items, int TotalCount)> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
    Task SaveSearchHistoryAsync(SearchHistory history, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SearchHistory>> GetSearchHistoryAsync(string userName, int limit = 50, CancellationToken cancellationToken = default);
    Task SaveErrorReportAsync(ErrorReport report, CancellationToken cancellationToken = default);
}
