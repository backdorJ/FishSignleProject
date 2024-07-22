namespace FishShop.Core.Services.NextFactory;

public class NextFactory : INextFactory
{
    /// <inheritdoc />
    public string GetNextFactory() => Random.Shared.Next(100, 10000).ToString();
}