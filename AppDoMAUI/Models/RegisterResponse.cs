namespace AppDoMAUI.Models;

public class RegisterResponse
{
    public bool Exitoso { get; set; }

    public string Mensaje { get; set; } = string.Empty;

    public int? UsuarioId { get; set; }
}