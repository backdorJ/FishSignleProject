using FishShop.Contracts.Enums;
using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishShop.DAL.Configurations;

/// <summary>
/// Конфигурация для товара
/// </summary>
public class ProductConfiguration : EntityBaseConfiguration<Product>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products", "other");
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasComment("Наименование товара")
            .HasColumnName("name")
            .HasMaxLength(200);

        builder.Property(p => p.Type)
            .HasComment("Тип товара")
            .HasColumnName("type")
            .HasConversion(
                to => to.ToString(),
                from => (CategoryType)Enum.Parse(typeof(CategoryType), from));
    }
}