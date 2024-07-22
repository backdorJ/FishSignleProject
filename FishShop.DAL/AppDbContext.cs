using FishShop.Core.Abstractions;
using FishShop.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishShop.DAL;

public class AppDbContext : DbContext, IDbContext
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options">Настрйоки из вне</param>
    /// <param name="mediator">Медиатор CQRS</param>
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
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

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await SaveChangesAsync(true, cancellationToken);

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<Entity>()
            .ToArray();

        var domainEvents = entries
            .Select(x => x.Entity)
            .OfType<Entity>()
            .SelectMany(x => x.GetDomainEvents())
            .ToArray();

        try
        {
            await Database.BeginTransactionAsync(cancellationToken);

            foreach (var @event in domainEvents)
                await _mediator.Publish(@event, cancellationToken);

            await Database.CommitTransactionAsync(cancellationToken);
            var result =  await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }
        catch (Exception e)
        {
            await Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}