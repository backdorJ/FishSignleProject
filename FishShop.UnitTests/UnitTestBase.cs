using System.Security.Claims;
using FishShop.Core.Entities;
using FishShop.Core.Services.JWTService;
using FishShop.Core.Services.PasswordService;
using FishShop.Core.Services.UserClaimsManager;
using FishShop.DAL;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FishShop.UnitTests;

public abstract class UnitTestBase : IDisposable
{
    /// <summary>
    /// Мок сервиса хеширования паролей
    /// </summary>
    protected Mock<IPasswordService> PasswordService { get; }
    
    /// <summary>
    /// Мок сервиса claims
    /// </summary>
    protected Mock<IUserMangerService> UserManager { get; }
    
    /// <summary>
    /// Мок сервиса токенов
    /// </summary>
    protected Mock<IJwtGenerator> JwtGenerator { get; }

    protected UnitTestBase()
    {
        UserManager = new Mock<IUserMangerService>();
        UserManager
            .Setup(x => x.GetUserClaims(It.IsAny<User>()))
            .Returns(new List<Claim>());

        JwtGenerator = new Mock<IJwtGenerator>();
        JwtGenerator.Setup(x => x.GenerateToken(It.IsAny<List<Claim>>()))
            .Returns("sadasd");
        
        PasswordService = new Mock<IPasswordService>();
        PasswordService
            .Setup(x => x.HashPassword(It.IsAny<string>()))
            .Returns<string>(x => x);
        PasswordService
            .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
    }
    
    protected AppDbContext CreateInMemoryContext(Action<AppDbContext>? seeder = null)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var appDbContext = new AppDbContext(options);
        
        seeder?.Invoke(appDbContext);
        appDbContext.SaveChanges();
        
        return appDbContext;
    } 
    
    /// <inheritdoc />
    public void Dispose() => GC.SuppressFinalize(this);
}