using FishShop.Core.Abstractions;
using FishShop.Core.Entities;
using FishShop.Core.Requests.AuthRequests.ConfirmEmailCode;
using Microsoft.EntityFrameworkCore;

namespace FishShop.UnitTests.Requests.AuthRequests;

/// <summary>
/// Тест для <see cref="ConfirmEmailCodeCommandHandler"/>
/// </summary>
public class ConfirmEmailCodeCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    
    public ConfirmEmailCodeCommandHandlerTest()
    {
        var entry = User.CreateForTest(
            email: "zalupka@mail.ru",
            code: "123",
            detail: new UserDetail
            {
                FirstName = "zalupa",
                LastName = "zalupa",
            });
        
        _dbContext = CreateInMemoryContext(x => x.AddRange(entry));
    }

    /// <summary>
    /// Должен принять код
    /// </summary>
    [Fact]
    public async Task Handle_ShouldSubmitCode()
    {
        var request = new ConfirmEmailCodeCommand
        {
            Email = "zalupka@mail.ru",
            Code = "123"
        };

        var handler = new ConfirmEmailCodeCommandHandler(_dbContext);
        await handler.Handle(request, default);

        var response = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        Assert.NotNull(response);

        Assert.Null(response.TempEmailCode);
    }
}