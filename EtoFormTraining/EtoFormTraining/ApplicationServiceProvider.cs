using Microsoft.Extensions.DependencyInjection;

namespace EtoFormTraining
{
    public static class ApplicationServiceProvider
    {
        public static T GetService<T>()
            where T : class
        {
            var collection = new ServiceCollection();
            collection.AddScoped<MainHostedService>();
            var services = collection.BuildServiceProvider();

            return services.GetRequiredService<MainHostedService>() as T;
        }
    }
}