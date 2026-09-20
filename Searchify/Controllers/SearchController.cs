using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Searchify.Application.Queries;
using Searchify.Domain.Model;

namespace Searchify.API.Controllers;

[ApiController]
[Route("api/search")]
[Authorize]
public sealed class SearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<SearchResponse>> Search([FromBody] SearchRequest request, CancellationToken cancellationToken)
    {
        var userName = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "anonymous";
        return Ok(await _mediator.Send(new SearchProductsQuery(request, userName), cancellationToken));
    }
}
