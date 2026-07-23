using AppDoMAUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AppDoMAUI;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
    }

    protected override Window CreateWindow(
        IActivationState? activationState)
    {
        LoginPage loginPage =
            _serviceProvider.GetRequiredService<LoginPage>();

        return new Window(
            new NavigationPage(loginPage));
    }
}