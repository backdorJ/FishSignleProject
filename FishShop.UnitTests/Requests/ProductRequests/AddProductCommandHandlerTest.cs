using FishShop.Contracts.Enums;
using FishShop.Core.Abstractions;
using FishShop.Core.Exceptions;
using FishShop.Core.Requests.ProductsRequests.AddProduct;

namespace FishShop.UnitTests.Requests.ProductRequests;

/// <summary>
/// Тест для <see cref="AddProductCommandHandler"/>
/// </summary>
public class AddProductCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    
    public AddProductCommandHandlerTest()
    {
        _dbContext = CreateInMemoryContext();
    }

    /// <summary>
    /// Метод должен создать товар по запросу
    /// </summary>
    [Fact]
    public async Task Handle_WithRequest_ShouldCreateEntity()
    {
        // Arrange 
        var request = new AddProductCommand
        {
            Name = "Test",
            Price = 1000,
            CategoryType = CategoryType.Rods
        };
        
        var handler = new AddProductCommandHandler(_dbContext);

        // Act
        var response = await handler.Handle(request, default);
        var entity = await _dbContext.Products.FindAsync(response.Id);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal(request.Name, entity.Name);
        Assert.Equal(request.Price, entity.Price);
        Assert.Equal(request.CategoryType, entity.Type);
    }

    /// <summary>
    /// Должен выбросить ошибку об некорректности аргументов 
    /// </summary>
    [Fact]
    public async Task Handle_WithUncorrectArgs_ShouldThrow()
    {
        var request = new AddProductCommand
        {
            Name = "test",
            Price = -1000,
            CategoryType = CategoryType.Rods
        };
        
        var handler = new AddProductCommandHandler(_dbContext);

        // Assert
        await Assert.ThrowsAsync<RequiredException>(() => handler.Handle(request, default));
    }
}