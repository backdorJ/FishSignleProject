using System.Collections.ObjectModel;

namespace FishShop.Core.Entities;

public class Role : Entity
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="roleName">Название роли</param>
    /// <param name="id">ИД</param>
    /// <param name="users">Пользователи</param>
    public Role(
        string roleName,
        Guid? id = default,
        List<User>? users = default)
        {
            RoleName = roleName;
            Id = id ?? Guid.NewGuid();
            Users = users ?? new();
        }

    private Role()
    {
    }

    /// <summary>
    /// Название роли
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// Связь ролей и привилегий
    /// </summary>
    public IReadOnlyCollection<RolePermission> RolePermissions { get; set; }

    /// <summary>
    /// Пользователи
    /// </summary>
    public List<User> Users { get; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="roleName">Название роли</param>
    /// <param name="id">Ид</param>
    /// <param name="users">Пользователи</param>
    /// <param name="createAt">Дата создания</param>
    /// <param name="updateAt">Дата обновления</param>
    /// <param name="rolePermissions">Список привилегий</param>
    /// <returns>Тестовая сущность</returns>
    [Obsolete("Только для тестов")]
    public static Role CreateForTest(
        string roleName,
        Guid? id = default,
        List<User>? users = default,
        DateTime? createAt = default,
        DateTime? updateAt = default,
        IReadOnlyCollection<RolePermission>? rolePermissions = default)
        => new(roleName: roleName, users: users)
        {
            Id = id ?? Guid.NewGuid(),
            CreateAt = createAt ?? DateTime.UtcNow,
            UpdateAt = updateAt,
            RoleName = roleName,
            RolePermissions = rolePermissions ?? new Collection<RolePermission>(),
        };
}