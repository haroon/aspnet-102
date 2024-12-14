// Data/ApplicationDbContext.cs
using aspnet102.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnet102.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Post> Posts { get; set; }
}
