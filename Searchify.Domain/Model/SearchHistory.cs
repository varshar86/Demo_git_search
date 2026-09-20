using System.ComponentModel.DataAnnotations;

namespace Searchify.Domain.Model;

public class SearchHistory
{
    [Key]
    public long Id { get; set; }
    [Required, MaxLength(100)]
    public string UserName { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Query { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    [MaxLength(50)]
    public string SortBy { get; set; } = "relevance";
    public int ResultCount { get; set; }
    public DateTime SearchedAtUtc { get; set; }
}
