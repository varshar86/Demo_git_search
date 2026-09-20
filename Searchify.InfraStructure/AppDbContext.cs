using Microsoft.EntityFrameworkCore;
using Searchify.Domain.Model;

namespace Searchify.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<SearchHistory> SearchHistories => Set<SearchHistory>();
    public DbSet<ErrorReport> ErrorReports => Set<ErrorReport>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
