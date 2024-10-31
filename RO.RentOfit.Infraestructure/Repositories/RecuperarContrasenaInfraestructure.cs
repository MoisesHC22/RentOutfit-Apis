
using RO.RentOfit.Domain.Aggregates.RecuperarContrasena;

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
                var tokenRecuperacion = token.FirstOrDefault()?.token;

                string baseUrl = "http://localhost:4200/OlvideMiContrasena";
                string emailParam = Uri.EscapeDataString(email);
                string tokenParam = Uri.EscapeDataString(tokenRecuperacion);
                string linkRecuperacion = $"{baseUrl}/{emailParam}?token={tokenParam}";

                string titulo = "Reestablecer contraseña de cuenta de RentOutfit";
                string mensaje = $@"
                 <html>
                  <body style='font-family: Arial, sans-serif;  color: #ffffff; margin: 0; padding: 0; display: flex; justify-content: center;'>
                   <div style='background-color: #094e6d; padding: 60px; max-width: 420px; margin: auto; text-align: center; border-radius: 8px;'>
                   <div style='font-size: 30px; font-weight: bold; margin-bottom: 20px; color: #ffffff;'>Reestablecer contraseña</div>
                   <div>
                     <img src='https://i.postimg.cc/KcJfkkqJ/Prueba1.jpg' alt='Prueba1' style='width: 150px; margin-bottom: 20px; border-radius: 20px;' />
                   </div>
                   <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 40px;'>
                     Recibimos una solicitud de reestablecimiento de tu contraseña. Para que este cambio se lleve a cabo, es necesario que lo confirmes.
                     Si ya no lo quieres realizar, puedes ignorar este correo electrónico.
                   </div>
                    <a href='{linkRecuperacion}' style='background-color: #ffffff; color: #000000; padding: 10px 20px; text-decoration: none; border-radius: 5px; font-weight: bold;display: inline-block; width: 80%; max-width: 250px; text-align: center;'>Reestablecer contraseña</a>
                   </div>
                  </body>
                 </html>";

                // Obtener el token de recuperación del primer resultado

                if (!string.IsNullOrEmpty(tokenRecuperacion))
                {
                    // Enviar el correo electrónico con el token de recuperación
                    await _emailService.EnviarMsj(email, titulo, mensaje);
                }

                return token.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al obtener el token de recuperación.", ex);
            }
        }

        public async Task<RespuestaDB> ValidarToken(ValidarToken requerimientos)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para validar el token
                var validacion = await _context.respuestaDB
                     .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Token @Email, @Token",
                     new SqlParameter("@Email", requerimientos.email), new SqlParameter("@Token", requerimientos.token))
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


        public async Task<RespuestaDB> ActualizarContrasena(ActualizarContrasena Requerimientos)
        {
            try
            {
                // Generar un nuevo token de seguridad
                var nuevoToken = Guid.NewGuid().ToString();

                // Ejecutar el procedimiento almacenado para actualizar la contraseña
                var actualizacion = await _context.respuestaDB
                     .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Actualizacion @Contrasena, @Email, @Token",
                     new SqlParameter("@Contrasena", Requerimientos.contrasena), new SqlParameter("@Email", Requerimientos.email), new SqlParameter("@Token", nuevoToken))
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
