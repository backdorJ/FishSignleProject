using FishShop.Core.Services.PasswordService;
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

    protected UnitTestBase()
    {
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