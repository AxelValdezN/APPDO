using AppDoMAUI.Models;
using System.Net;
using System.Net.Http.Json;

namespace AppDoMAUI.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync(
                    "api/auth/login",
                    request,
                    cancellationToken);

            LoginResponse? result =
                await response.Content.ReadFromJsonAsync<LoginResponse>(
                    cancellationToken: cancellationToken);

            if (result is not null)
            {
                return result;
            }

            return new LoginResponse
            {
                Exitoso = false,
                Mensaje = "La API devolvió una respuesta vacía."
            };
        }
        catch (HttpRequestException)
        {
            return new LoginResponse
            {
                Exitoso = false,
                Mensaje = "No fue posible conectarse con la API."
            };
        }
        catch (TaskCanceledException)
        {
            return new LoginResponse
            {
                Exitoso = false,
                Mensaje = "La solicitud tardó demasiado tiempo."
            };
        }
    }

    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync(
                    "api/auth/register",
                    request,
                    cancellationToken);

            RegisterResponse? result =
                await response.Content.ReadFromJsonAsync<RegisterResponse>(
                    cancellationToken: cancellationToken);

            if (result is not null)
            {
                return result;
            }

            return new RegisterResponse
            {
                Exitoso = false,
                Mensaje = "La API devolvió una respuesta vacía."
            };
        }
        catch (HttpRequestException)
        {
            return new RegisterResponse
            {
                Exitoso = false,
                Mensaje = "No fue posible conectarse con la API."
            };
        }
        catch (TaskCanceledException)
        {
            return new RegisterResponse
            {
                Exitoso = false,
                Mensaje = "La solicitud tardó demasiado tiempo."
            };
        }
    }
}