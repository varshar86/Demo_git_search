using Searchify.Domain.Model;

namespace Searchify.Domain.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task<SearchResponse> SearchAsync(SearchRequest request, string userName, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SearchHistory>> GetSearchHistoryAsync(string userName, int limit = 50, CancellationToken cancellationToken = default);
    Task SaveErrorReportAsync(ErrorReport report, CancellationToken cancellationToken = default);
}
