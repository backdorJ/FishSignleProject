using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishShop.Core.Abstractions;

public interface IDbContext
{
    /// <summary>
    /// Товары
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Роли
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Привилегии
    /// </summary>
    public DbSet<Permission> Permissions { get; set; }

    /// <summary>
    /// Связь между привилегиями и ролями
    /// </summary>
    public DbSet<RolePermission> RolePermissions { get; set; }
    
    /// <summary>
    /// Метод сохранения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>-</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}