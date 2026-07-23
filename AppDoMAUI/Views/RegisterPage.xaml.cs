using AppDoMAUI.Models;
using AppDoMAUI.Services;

namespace AppDoMAUI.Views;

public partial class RegisterPage : ContentPage
{
    private readonly AuthService _authService;

    public RegisterPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void OnRegisterClicked(
        object sender,
        EventArgs e)
    {
        string nombre =
            NameEntry.Text?.Trim() ?? string.Empty;

        string email =
            EmailEntry.Text?.Trim() ?? string.Empty;

        string password =
            PasswordEntry.Text ?? string.Empty;

        string confirmPassword =
            ConfirmPasswordEntry.Text ?? string.Empty;

        MessageLabel.Text = string.Empty;

        if (string.IsNullOrWhiteSpace(nombre) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageLabel.Text =
                "Completa todos los campos.";

            return;
        }

        if (!email.Contains('@'))
        {
            MessageLabel.Text =
                "Captura un correo válido.";

            return;
        }

        if (password.Length < 6)
        {
            MessageLabel.Text =
                "La contraseña debe tener al menos 6 caracteres.";

            return;
        }

        if (password != confirmPassword)
        {
            MessageLabel.Text =
                "Las contraseñas no coinciden.";

            return;
        }

        SetLoadingState(true);

        RegisterResponse response =
            await _authService.RegisterAsync(
                new RegisterRequest
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password
                });

        SetLoadingState(false);

        MessageLabel.Text = response.Mensaje;

        if (!response.Exitoso)
        {
            return;
        }

        await DisplayAlertAsync(
            "APPDO",
            "La cuenta se creó correctamente.",
            "Aceptar");

        await Navigation.PopAsync();
    }

    private async void OnBackToLoginClicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void SetLoadingState(bool isLoading)
    {
        RegisterButton.IsEnabled = !isLoading;
        LoadingIndicator.IsVisible = isLoading;
        LoadingIndicator.IsRunning = isLoading;

        NameEntry.IsEnabled = !isLoading;
        EmailEntry.IsEnabled = !isLoading;
        PasswordEntry.IsEnabled = !isLoading;
        ConfirmPasswordEntry.IsEnabled = !isLoading;

        if (isLoading)
        {
            MessageLabel.Text = "Registrando...";
        }
    }
}