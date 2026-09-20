using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;

namespace Searchify.API.Controllers;

[ApiController]
[Authorize]
[Route("api/error-reports")]
public class ErrorReportsController(IProductService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Report([FromBody] ErrorReport report, CancellationToken cancellationToken)
    {
        report.ReportedBy = User.FindFirstValue(ClaimTypes.Name) ?? "unknown";
        report.ReportedAtUtc = DateTime.UtcNow;
        report.TraceId ??= HttpContext.TraceIdentifier;
        await service.SaveErrorReportAsync(report, cancellationToken);
        return Accepted(new { message = "Error report submitted.", traceId = report.TraceId });
    }
}
