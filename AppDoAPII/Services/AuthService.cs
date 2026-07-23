using AppDoAPI.Data;
using AppDoAPI.Models.Entities;
using AppDoAPI.Models.Requests;
using AppDoAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace AppDoAPI.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        string emailNormalizado = request.Email.Trim().ToLowerInvariant();

        User? usuario = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user =>
                user.Email.ToLower() == emailNormalizado);

        if (usuario is null)
        {
            return new LoginResponse
            {
                Exitoso = false,
                Mensaje = "Usuario o contraseña incorrectos."
            };
        }

        bool passwordCorrecto = usuario.Password == request.Password;

        if (!passwordCorrecto)
        {
            return new LoginResponse
            {
                Exitoso = false,
                Mensaje = "Usuario o contraseña incorrectos."
            };
        }

        return new LoginResponse
        {
            Exitoso = true,
            Mensaje = "Autenticación exitosa.",
            Token = "TOKEN_TEMPORAL",
            UsuarioId = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        };
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        string emailNormalizado = request.Email.Trim().ToLowerInvariant();

        bool emailExiste = await _context.Users
            .AnyAsync(user =>
                user.Email.ToLower() == emailNormalizado);

        if (emailExiste)
        {
            return new RegisterResponse
            {
                Exitoso = false,
                Mensaje = "Ya existe un usuario registrado con ese correo."
            };
        }

        var usuario = new User
        {
            Nombre = request.Nombre.Trim(),
            Email = emailNormalizado,
            Password = request.Password,
            FechaRegistro = DateTime.UtcNow
        };

        _context.Users.Add(usuario);
        await _context.SaveChangesAsync();

        return new RegisterResponse
        {
            Exitoso = true,
            Mensaje = "Usuario registrado correctamente.",
            UsuarioId = usuario.Id
        };
    }
}