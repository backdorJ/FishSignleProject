using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishShop.DAL.Configurations;

/// <summary>
/// Конфигурация для привилегии
/// </summary>
public class PermissionConfiguration : EntityBaseConfiguration<Permission>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        
        builder.Property(p => p.Code)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired();

        builder.HasMany(x => x.RolesPermissions)
            .WithOne(y => y.Permission)
            .HasForeignKey(y => y.PermissionId)
            .HasPrincipalKey(x => x.Id);
    }
}