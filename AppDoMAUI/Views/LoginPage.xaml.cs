using AppDoMAUI.Models;
using AppDoMAUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppDoMAUI.Views;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;
    private readonly IServiceProvider _serviceProvider;

    public LoginPage(
        AuthService authService,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _authService = authService;
        _serviceProvider = serviceProvider;
    }

    private async void OnLoginClicked(
        object sender,
        EventArgs e)
    {
        string email =
            EmailEntry.Text?.Trim() ?? string.Empty;

        string password =
            PasswordEntry.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            MessageLabel.Text =
                "Captura el correo y la contraseña.";

            return;
        }

        SetLoadingState(true);
        MessageLabel.Text = string.Empty;

        LoginResponse response =
            await _authService.LoginAsync(
                new LoginRequest
                {
                    Email = email,
                    Password = password
                });

        SetLoadingState(false);
        MessageLabel.Text = response.Mensaje;

        if (response.Exitoso)
        {
            await DisplayAlertAsync(
                "APPDO",
                $"Bienvenido, {response.Nombre}.",
                "Aceptar");
        }

    }

    private async void OnRegisterClicked(
        object sender,
        EventArgs e)
    {
        RegisterPage registerPage =
     _serviceProvider.GetRequiredService<RegisterPage>();

        await Navigation.PushAsync(registerPage);
    }
    private void SetLoadingState(bool isLoading)
    {
        LoginButton.IsEnabled = !isLoading;

        EmailEntry.IsEnabled = !isLoading;
        PasswordEntry.IsEnabled = !isLoading;

        LoadingIndicator.IsVisible = isLoading;
        LoadingIndicator.IsRunning = isLoading;
    }
}