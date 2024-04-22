using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Company> Companies { get; set; } = null;

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}