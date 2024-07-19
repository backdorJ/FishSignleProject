using FishShop.Core.Abstractions;
using FishShop.Core.Constants;
using FishShop.Core.Entities;
using FishShop.Core.Requests.AuthRequests.PostRegister;
using Microsoft.EntityFrameworkCore;

namespace FishShop.UnitTests.Requests.AuthRequests;

/// <summary>
/// Тест для <see cref="PostRegisterCommandHandler"/>
/// </summary>
public class PostRegisterCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;

    public PostRegisterCommandHandlerTest()
    {
        var role = Role.CreateForTest(
            id: DefaultRolesIds.User,
            roleName: "test");
        
        _dbContext = CreateInMemoryContext(
            x => x.AddRange(role));
    }

    /// <summary>
    /// Обработчик должен создать пользователя
    /// </summary>
    [Fact]
    public async Task Handle_ShouldCreateUser()
    {
        var request = new PostRegisterCommand
        {
            Username = "test",
            Email = "email@mail.ru",
            Password = "123123123",
            UserDetails = new()
            {
                FirstName = "test",
                LastName = "test",
                Patronymic = "test",
                BirthDate = DateTime.UtcNow
            }
        };

        var handler = new PostRegisterCommandHandler(_dbContext, PasswordService.Object);
        await handler.Handle(request, default);

        var response = await _dbContext.Users.FirstOrDefaultAsync();

        Assert.NotNull(response);
        Assert.NotNull(response.Details);
        
        Assert.NotEmpty(response.HashPassword);
        
        Assert.Equal(request.Username, response.UserName);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.UserDetails.FirstName, response.Details.FirstName);
        Assert.Equal(request.UserDetails.LastName, response.Details.LastName);
        Assert.Equal(request.UserDetails.Patronymic, response.Details.Patronymic);
        Assert.Equal(request.UserDetails.BirthDate, response.Details.BirthDate);
    }
}