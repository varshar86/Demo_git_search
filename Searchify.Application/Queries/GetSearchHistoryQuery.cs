using MediatR;
using Searchify.Domain.Model;

namespace Searchify.Application.Queries;

public sealed record GetSearchHistoryQuery(string UserName, int Limit = 50) : IRequest<IReadOnlyList<SearchHistory>>;
