using FishShop.Core.Constants;
using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishShop.DAL.Configurations;

/// <summary>
/// Конфигурация для пользователя
/// </summary>
public class UserConfiguration : EntityBaseConfiguration<User>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "secure");
        
        builder.Property(p => p.Email)
            .IsRequired()
            .HasComment("Почта")
            .HasColumnName("email");

        builder.Property(p => p.UserName)
            .IsRequired()
            .HasComment("Логин")
            .HasColumnName("username");

        builder.Property(p => p.HashPassword)
            .IsRequired()
            .HasColumnName("hash")
            .HasComment("Хеш пароля");

        builder.Property(p => p.TempEmailCode)
            .HasComment("Код подтверждения для почты")
            .HasColumnName("temp_email_code");

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasComment("Статус регистрации")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasConversion(
                v => v.ToString(),
                e => (UserRegisterStatus)Enum.Parse(typeof(UserRegisterStatus), e));

        builder.OwnsOne(x => x.Details, conf =>
        {
            conf.Property(p => p.FirstName)
                .HasComment("Имя")
                .HasColumnName("first_name")
                .IsRequired();
            
            conf.Property(p => p.LastName)
                .HasComment("Фамилия")
                .HasColumnName("last_name")
                .IsRequired();

            conf.Property(p => p.BirthDate)
                .HasComment("День рождение пользователя")
                .HasColumnName("birth_date");

            conf.Property(p => p.Patronymic)
                .HasComment("Отчество")
                .HasColumnName("patronymic");
        });

        builder.HasMany(x => x.Roles)
            .WithMany(y => y.Users)
            .UsingEntity(x => x
                .ToTable("users_roles")
                .HasComment("Связь между пользователями и ролями"));
    }
}