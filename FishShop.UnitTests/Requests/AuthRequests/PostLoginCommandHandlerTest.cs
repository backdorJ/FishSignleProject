using FishShop.Core.Abstractions;
using FishShop.Core.Entities;
using FishShop.Core.Requests.AuthRequests.PostLogin;

namespace FishShop.UnitTests.Requests.AuthRequests;

/// <summary>
/// Тест для <see cref="PostLoginCommandHandler"/>
/// </summary>
public class PostLoginCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly User _user;

    public PostLoginCommandHandlerTest()
    {
        _user = User.CreateForTest(
            email: "email@mail.ru",
            hashPassword: "123",
            detail: UserDetail.CreateForTest(
                firstName: "asdad",
                lastName: "asdasd"));
        
        _dbContext = CreateInMemoryContext(x => x.AddRange(_user));
    } 
    
    /// <summary>
    /// Должен создать токен
    /// </summary>
    [Fact]
    public async Task Handle_ShouldCreateToken()
    {
        var request = new PostLoginCommand
        {
            Email = "email@mail.ru",
            Password = "1213"
        };

        var handler = new PostLoginCommandHandler(
            _dbContext,
            UserManager.Object,
            JwtGenerator.Object,
            PasswordService.Object);

        var response = await handler.Handle(request, default);

        Assert.NotNull(response.AccessToken);
    }
}