using Microsoft.Extensions.DependencyInjection;

namespace FishShop.RabbitMQ;

public static class Entry
{
    public static void AddRabbitMQ(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPublisher, Publisher>();
    }
}