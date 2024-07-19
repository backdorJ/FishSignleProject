using FishShop.Core.Abstractions;
using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishShop.DAL;

public class AppDbContext : DbContext, IDbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options">Настрйоки из вне</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    /// <inheritdoc />
    public DbSet<Product> Products { get; set; }

    /// <inheritdoc />
    public DbSet<User> Users { get; set; }

    /// <inheritdoc />
    public DbSet<Role> Roles { get; set; }

    /// <inheritdoc />
    public DbSet<Permission> Permissions { get; set; }

    /// <inheritdoc />
    public DbSet<RolePermission> RolePermissions { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}