using RO.RentOfit.API.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class RecuperarContrasenaInfraestructure : IRecuperarContrasenaInfraestructure
    {
        private readonly RentOutfitContext _context;
        private readonly EmailService _emailService;

        // Constructor que inyecta el contexto y el servicio de email
        public RecuperarContrasenaInfraestructure(RentOutfitContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Método para obtener el token de recuperación de contraseña
        public async Task<RecuperarContrasenaDto> ObtenerToken(string email)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para obtener el token
                var token = await _context.recuperarContrasenaDto
                    .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Email @Email", new SqlParameter("@Email", email))
                    .ToListAsync();

                if (token == null || !token.Any())
                {
                    return null;
                }

                // Obtener el token de recuperación del primer resultado
                var tokenRecuperacion = token.FirstOrDefault()?.token;
                if (!string.IsNullOrEmpty(tokenRecuperacion))
                {
                    // Enviar el correo electrónico con el token de recuperación
                    await _emailService.EnviarEmailRecuperacionContrasenaAsync(email, tokenRecuperacion);
                }

                return token.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al obtener el token de recuperación.", ex);
            }
        }

        // Método para validar el token de recuperación
        public async Task<RespuestaDB> ValidarToken(string email, string token)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para validar el token
                var validacion = await _context.respuestaDB
                     .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Token @Email, @Token",
                     new SqlParameter("@Email", email), new SqlParameter("@Token", token))
                     .ToListAsync();

                if (validacion == null || !validacion.Any())
                {
                    return null;
                }

                return validacion.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al validar el token.", ex);
            }
        }

        // Método para actualizar la contraseña
        public async Task<RespuestaDB> ActualizarContrasena(string contrasena, string email)
        {
            try
            {
                // Generar un nuevo token de seguridad
                var nuevoToken = Guid.NewGuid().ToString();

                // Ejecutar el procedimiento almacenado para actualizar la contraseña
                var actualizacion = await _context.respuestaDB
                     .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Actualizacion @Contrasena, @Email, @Token",
                     new SqlParameter("@Contrasena", contrasena), new SqlParameter("@Email", email), new SqlParameter("@Token", nuevoToken))
                     .ToListAsync();

                if (actualizacion == null || !actualizacion.Any())
                {
                    return null;
                }

                return actualizacion.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al actualizar la contraseña.", ex);
            }
        }
    }
}
