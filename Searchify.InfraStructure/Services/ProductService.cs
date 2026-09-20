using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;

namespace Searchify.Infrastructure.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

    public Task<IEnumerable<Product>> GetAllProducts() => _productRepository.GetAllProducts();

    public Task<Product?> GetProductById(int id) => _productRepository.GetProductById(id);

    public async Task<SearchResponse> SearchAsync(SearchRequest request, string userName, CancellationToken cancellationToken = default)
    {
        if (request.MinPrice.HasValue && request.MaxPrice.HasValue && request.MinPrice > request.MaxPrice)
            throw new ArgumentException("MinPrice cannot be greater than MaxPrice.");

        var (items, total) = await _productRepository.SearchAsync(request, cancellationToken);
        var page = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        await _productRepository.SaveSearchHistoryAsync(new SearchHistory
        {
            UserName = string.IsNullOrWhiteSpace(userName) ? "anonymous" : userName,
            Query = request.Query ?? string.Empty,
            Category = request.Category,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            SortBy = request.SortBy,
            ResultCount = total,
            SearchedAtUtc = DateTime.UtcNow
        }, cancellationToken);

        return new SearchResponse
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public Task<IReadOnlyList<SearchHistory>> GetSearchHistoryAsync(string userName, int limit = 50, CancellationToken cancellationToken = default) =>
        _productRepository.GetSearchHistoryAsync(userName, limit, cancellationToken);

    public Task SaveErrorReportAsync(ErrorReport report, CancellationToken cancellationToken = default) =>
        _productRepository.SaveErrorReportAsync(report, cancellationToken);
}
