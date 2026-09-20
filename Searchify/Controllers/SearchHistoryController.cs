using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Searchify.Application.Queries;
using Searchify.Domain.Model;

namespace Searchify.API.Controllers;

[Route("api/search-history")]
[ApiController]
[Authorize]
public sealed class SearchHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchHistoryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SearchHistory>>> Get([FromQuery] int limit = 50, CancellationToken cancellationToken = default)
    {
        var userName = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "anonymous";
        return Ok(await _mediator.Send(new GetSearchHistoryQuery(userName, limit), cancellationToken));
    }
}
