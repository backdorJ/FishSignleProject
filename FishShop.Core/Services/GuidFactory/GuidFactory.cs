namespace FishShop.Core.Services.GuidFactory;

public class GuidFactory : IGuidFactory
{
    /// <inheritdoc />
    public Guid GetGuid() => Guid.NewGuid();
}