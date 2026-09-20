using System.ComponentModel.DataAnnotations;

namespace Searchify.Domain.Model;

public class ErrorReport
{
    [Key]
    public long Id { get; set; }
    [Required, MaxLength(2000)]
    public string Message { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Endpoint { get; set; }
    [MaxLength(100)]
    public string? TraceId { get; set; }
    [MaxLength(2000)]
    public string? Details { get; set; }
    [MaxLength(100)]
    public string ReportedBy { get; set; } = string.Empty;
    public DateTime ReportedAtUtc { get; set; }
}
