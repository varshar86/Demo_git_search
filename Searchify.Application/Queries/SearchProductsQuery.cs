using MediatR;
using Searchify.Domain.Model;

namespace Searchify.Application.Queries;

public sealed record SearchProductsQuery(SearchRequest Request, string UserName) : IRequest<SearchResponse>;
