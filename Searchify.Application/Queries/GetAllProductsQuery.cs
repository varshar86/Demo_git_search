using MediatR;
using Searchify.Domain.Model;

namespace Searchify.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {

    }
}
