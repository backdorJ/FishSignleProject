using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishShop.DAL.Configurations;

/// <summary>
/// Конфигурация для связи ролей и привилегий
/// </summary>
public class RolePermissionConfiguration : EntityBaseConfiguration<RolePermission>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("roles_permissions");
        
        builder.HasOne(x => x.Permission)
            .WithMany(y => y.RolesPermissions)
            .HasForeignKey(x => x.PermissionId)
            .HasPrincipalKey(y => y.Id);

        builder.HasOne(x => x.Role)
            .WithMany(y => y.RolePermissions)
            .HasForeignKey(x => x.RoleId)
            .HasPrincipalKey(y => y.Id);
    }
}