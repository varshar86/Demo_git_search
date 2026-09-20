using MediatR;
using Searchify.Application.Queries;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;

namespace Searchify.Application.CommandHandlers;

public sealed class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductService _productService;

    public GetAllProductsCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        => _productService.GetAllProducts();
}
