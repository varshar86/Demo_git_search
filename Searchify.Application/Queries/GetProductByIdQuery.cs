using MediatR;
using Searchify.Domain.Model;

namespace Searchify.Application.Queries;

public sealed class GetProductByIdQuery : IRequest<Product?>
{
    public int ProductId { get; }

    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}
