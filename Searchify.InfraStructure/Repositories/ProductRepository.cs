using Microsoft.EntityFrameworkCore;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;

namespace Searchify.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Product>> GetAllProducts() =>
        await _context.Products.AsNoTracking().ToListAsync();

    public Task<Product?> GetProductById(int id) =>
        _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);

    public async Task<(IReadOnlyList<Product> Items, int TotalCount)> SearchAsync(
        SearchRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = _context.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            var term = request.Query.Trim().ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(term) ||
                (p.Description != null && p.Description.ToLower().Contains(term)) ||
                (p.Category != null && p.Category.ToLower().Contains(term)));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(p => p.Category == request.Category);

        if (request.MinPrice.HasValue)
            query = query.Where(p => p.Price >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= request.MaxPrice.Value);

        var total = await query.CountAsync(cancellationToken);
        var descending = !string.Equals(request.SortDirection, "asc", StringComparison.OrdinalIgnoreCase);

        query = request.SortBy.ToLowerInvariant() switch
        {
            "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
            "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "stock" or "popularity" => descending ? query.OrderByDescending(p => p.Stock) : query.OrderBy(p => p.Stock),
            _ => descending ? query.OrderByDescending(p => p.ProductId) : query.OrderBy(p => p.ProductId)
        };

        var page = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task SaveSearchHistoryAsync(SearchHistory history, CancellationToken cancellationToken = default)
    {
        _context.SearchHistories.Add(history);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveErrorReportAsync(ErrorReport report, CancellationToken cancellationToken = default)
    {
        _context.ErrorReports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SearchHistory>> GetSearchHistoryAsync(
        string userName, int limit = 50, CancellationToken cancellationToken = default)
    {
        limit = Math.Clamp(limit, 1, 200);
        return await _context.SearchHistories
            .AsNoTracking()
            .Where(x => x.UserName == userName)
            .OrderByDescending(x => x.SearchedAtUtc)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}
