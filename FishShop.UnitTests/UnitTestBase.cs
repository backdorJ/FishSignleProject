using System.Security.Claims;
using FishShop.Contracts.Models;
using FishShop.Core.Entities;
using FishShop.Core.Services.GuidFactory;
using FishShop.Core.Services.JWTService;
using FishShop.Core.Services.NextFactory;
using FishShop.Core.Services.PasswordService;
using FishShop.Core.Services.UserClaimsManager;
using FishShop.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using IPublisher = FishShop.RabbitMQ.IPublisher;

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
    
    /// <summary>
    /// Мок сервиса Guid
    /// </summary>
    protected Mock<IGuidFactory> GuidFactory { get; }
    
    /// <summary>
    /// Мок сервиса брокера 
    /// </summary>
    protected Mock<IPublisher> Publisher { get; }
    
    /// <summary>
    /// Mock сервиса рандомный чисел
    /// </summary>
    protected Mock<INextFactory> NextFactory { get; }
    
    /// <summary>
    /// Mock медиатора
    /// </summary>
    protected Mock<IMediator> MediatR { get; }

    protected UnitTestBase()
    {
        MediatR = new Mock<IMediator>();
        
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

        GuidFactory = new Mock<IGuidFactory>();
        GuidFactory.Setup(x => x.GetGuid())
            .Returns(Guid.Parse("CA6CAD75-2A9C-44F0-B1DC-02ECB98EE3CD"));

        Publisher = new Mock<IPublisher>();
        Publisher.Setup(x => x.Send(It.IsAny<QueueRequest>()))
            .Verifiable();

        NextFactory = new Mock<INextFactory>();
        NextFactory.Setup(x => x.GetNextFactory())
            .Returns("1234");
    }
    
    protected AppDbContext CreateInMemoryContext(Action<AppDbContext>? seeder = null)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var appDbContext = new AppDbContext(
            options,
            MediatR.Object);
        
        seeder?.Invoke(appDbContext);
        appDbContext.SaveChanges();
        
        return appDbContext;
    } 
    
    /// <inheritdoc />
    public void Dispose() => GC.SuppressFinalize(this);
}