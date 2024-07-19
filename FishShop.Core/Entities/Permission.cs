namespace FishShop.Core.Entities;

/// <summary>
/// Привилегия
/// </summary>
public class Permission : Entity
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Наименование</param>
    /// <param name="code">Код</param>
    public Permission(string name, string code)
    {
        Name = name;
        Code = code;
    }

    /// <summary>
    /// Название привилегии
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Код привилегии
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Связь между ролями и привилегиями
    /// </summary>
    public IReadOnlyCollection<RolePermission> RolesPermissions { get; set; }
}