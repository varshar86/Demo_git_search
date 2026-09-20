using MediatR;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;
using Searchify.Application.Queries;

namespace Searchify.Application.Handlers;

public sealed class GetProductByIdQueryHandler(IProductService productService)
    : IRequestHandler<GetProductByIdQuery, Product?>
{
    public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        productService.GetProductById(request.ProductId);
}
