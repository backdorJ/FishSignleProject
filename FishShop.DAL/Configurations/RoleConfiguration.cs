using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishShop.DAL.Configurations;

/// <summary>
/// Конфигурация для роли
/// </summary>
public class RoleConfiguration : EntityBaseConfiguration<Role>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        
        builder.Property(p => p.RoleName)
            .IsRequired();

        builder.HasMany(x => x.RolePermissions)
            .WithOne(y => y.Role)
            .HasForeignKey(y => y.RoleId)
            .HasPrincipalKey(x => x.Id);

        builder.HasMany(y => y.Users)
            .WithMany(y => y.Roles)
            .UsingEntity(x => x
                .ToTable("users_roles")
                .HasComment("Связь между пользователями и ролями"));
    }
}