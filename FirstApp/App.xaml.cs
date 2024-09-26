using FirstApp.Data;
using FirstApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FirstApp;
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureService(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureService(IServiceCollection services)
    {
        // Register view models
        services.AddTransient<MainViewModel>();
        services.AddTransient<CustomersViewModel>();
        services.AddTransient<ProductsViewModel>();

        // Register data providers or services
        services.AddTransient<ICustomerDataProvider, CustomerDataProvider>();
        services.AddTransient<IProductDataProvider, ProductDataProvider>();

        // Register MainWindow with its dependencies
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Resolve MainWindow from service provider
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

        // Show MainWindow
        mainWindow?.Show();
    }
}
