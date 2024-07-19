namespace FishShop.Core.Entities;

/// <summary>
/// Связь между ролями и привилегиями
/// </summary>
public class RolePermission : Entity
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="role">Роль</param>
    /// <param name="permission">Привилегия</param>
    public RolePermission(Role? role, Permission? permission)
    {
        Role = role;
        Permission = permission;
    }

    private RolePermission()
    {
    }

    /// <summary>
    /// ИД роли
    /// </summary>
    public Guid? RoleId { get; set; }

    /// <summary>
    /// ИД привилегии
    /// </summary>
    public Guid? PermissionId { get; set; }

    #region Navigation Properties
    
    /// <summary>
    /// Роль
    /// </summary>
    public Role? Role { get; set; }

    /// <summary>
    /// Привилегия
    /// </summary>
    public Permission? Permission { get; set; }
    
    #endregion
}