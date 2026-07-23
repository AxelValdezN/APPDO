namespace AppDoMAUI.Models;

public class LoginResponse
{
    public bool Exitoso { get; set; }

    public string Mensaje { get; set; } = string.Empty;

    public string? Token { get; set; }

    public int? UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }
}