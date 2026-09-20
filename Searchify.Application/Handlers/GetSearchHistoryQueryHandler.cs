using MediatR;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;
using Searchify.Application.Queries;

namespace Searchify.Application.Handlers;

public sealed class GetSearchHistoryQueryHandler(IProductService productService)
    : IRequestHandler<GetSearchHistoryQuery, IReadOnlyList<SearchHistory>>
{
    public Task<IReadOnlyList<SearchHistory>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken) =>
        productService.GetSearchHistoryAsync(request.UserName, request.Limit, cancellationToken);
}
