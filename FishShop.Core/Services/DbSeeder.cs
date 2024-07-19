using System.ComponentModel;
using System.Reflection;
using FishShop.Core.Abstractions;
using FishShop.Core.Constants;
using FishShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishShop.Core.Services;

/// <summary>
/// Seed базовых данных
/// </summary>
public class DbSeeder
{
    private readonly IDbContext _dbContext;

    private readonly IReadOnlyDictionary<Guid, string> _baseRoles
        = new Dictionary<Guid, string>
        {
            [DefaultRolesIds.Admin] = GetDescriptionValue(nameof(DefaultRolesIds.Admin), typeof(DefaultRolesIds)),
            [DefaultRolesIds.User] = GetDescriptionValue(nameof(DefaultRolesIds.User), typeof(DefaultRolesIds))
        };
     
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public DbSeeder(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await SeedRolesAsync(cancellationToken);
        await SeedPermissionsAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(CancellationToken cancellationToken)
    {
        var rolesInDb = await _dbContext.Roles
            .ToListAsync(cancellationToken);

        var rolesToRemove = rolesInDb
            .Where(x => !_baseRoles.ContainsKey(x.Id))
            .ToList();

        var rolesToAdd = _baseRoles
            .Where(x => rolesInDb.All(y => y.Id != x.Key))
            .Select(x => new
            {
                x.Key,
                x.Value
            })
            .ToList();
        
        if (rolesToRemove.Any())
            _dbContext.Roles.RemoveRange(rolesToRemove);
        
        rolesToAdd.ForEach(x =>
        {
            var newRole = new Role(x.Value, x.Key);
            _dbContext.Roles.Add(newRole);
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedPermissionsAsync(CancellationToken cancellationToken)
    {
        var permissionsAndRoles = DefaultRolesIds.RolesToPermissions;
        
        foreach (var (role, permissions) in permissionsAndRoles)
        {
            var roleInDb = await _dbContext.Roles
                .Include(x => x.RolePermissions)
                    .ThenInclude(y => y.Permission)
                .Where(x => x.Id == role)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new ApplicationException("Такой роли не найдено");

            var perToDel = roleInDb
                .RolePermissions
                .Where(x => !permissions.Contains(x.Permission!.Code))
                .ToList();

            var perToAdd = permissions
                .Where(x => roleInDb.RolePermissions.All(y => y.Permission!.Code != x))
                .ToList();
                
            foreach (var permission in perToAdd)
            {
                var perVal = GetFieldValue(permission, typeof(PermissionsConstants));
                var rolePermission = new RolePermission(
                    roleInDb,
                    new Permission(perVal, permission));

                _dbContext.RolePermissions.Add(rolePermission);
            }

            if (perToDel.Any())
                _dbContext.RolePermissions.RemoveRange(perToDel);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static string GetDescriptionValue(string fieldName, Type type)
    {
        var memberInfo = type.GetMember(fieldName)
        ??  throw new ApplicationException($"Не удалось найти поле в классе: {type.FullName}");

        var attribute = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attribute.Length < 1 ? fieldName : ((DescriptionAttribute)attribute[0]).Description;
    }

    private static string GetFieldValue(string fieldName, Type type)
    {
        var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static)
                    ?? throw new ApplicationException($"Не удалось найти поле '{fieldName}' в типе '{type.FullName}'.");

        return (string)field.GetValue(null) ?? "string";
    }
}