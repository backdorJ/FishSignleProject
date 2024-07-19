using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishShop.DAL.Configurations;

/// <summary>
/// Базовая конфигурация для сущностей
/// </summary>
public abstract class EntityBaseConfiguration<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    private const string NowCommand = "now() at time zone 'utc'";
    
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.CreateAt)
            .IsRequired()
            .HasComment("Дата создания")
            .HasColumnName("create_at")
            .HasDefaultValueSql(NowCommand);

        builder.Property(p => p.UpdateAt)
            .HasComment("Дата обновления")
            .HasColumnName("update_at");

        ConfigureChild(builder);
    }

    /// <summary>
    /// Добавить настройки к сущности
    /// </summary>
    protected abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
}