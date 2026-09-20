using MediatR;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;
using Searchify.Application.Queries;

namespace Searchify.Application.Handlers;

public sealed class SearchProductsQueryHandler(IProductService productService)
    : IRequestHandler<SearchProductsQuery, SearchResponse>
{
    public Task<SearchResponse> Handle(SearchProductsQuery request, CancellationToken cancellationToken) =>
        productService.SearchAsync(request.Request, request.UserName, cancellationToken);
}
