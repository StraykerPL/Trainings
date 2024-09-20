using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WinFormsMVVM
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();

            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MainFormHostedService>();
                    services.AddScoped<CounterModel>();
                    services.AddScoped<ClickViewModel>();
                    services.AddScoped<Form>();
                    services.AddScoped<MainView>();
                })
                .Build();

            host.Start();
        }
    }
}