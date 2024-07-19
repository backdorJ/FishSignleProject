using System.ComponentModel;

namespace FishShop.Core.Constants;

/// <summary>
/// Роли
/// </summary>
public static class DefaultRolesIds
{
    /// <summary>
    /// Админ
    /// </summary>
    [Description("Администратор")]
    public static Guid Admin = Guid.Parse("E83F1065-BAA5-41BD-8884-0154F1654DE1");

    /// <summary>
    /// Пользователь
    /// </summary>
    [Description("Пользователь")]
    public static Guid User = Guid.Parse("E414FB8E-C784-4FB4-A871-8F7128BD94D4");

    public static readonly IReadOnlyDictionary<Guid, List<string>> RolesToPermissions
        = new Dictionary<Guid, List<string>>
        {
            [Admin] = new()
            {
                PermissionsConstants.CreateProductPermission
            }
        };
}