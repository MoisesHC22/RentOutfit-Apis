
namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class RecuperarContrasenaInfraestructure : IRecuperarContrasenaInfraestructure
    {
        private readonly RentOutfitContext _context;
        
        public RecuperarContrasenaInfraestructure(RentOutfitContext context)
        {
            _context = context;
        }



        public async Task<RecuperarContrasenaDto> ObtenerToken(string email) 
        {
            try
            {
                var token = await _context.recuperarContrasenaDto
                    .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Email @email ", new SqlParameter("@email", email))
                    .ToListAsync();

                if (token == null || !token.Any())
                {
                    return null;
                }

                return token.FirstOrDefault();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }



        public async Task<RespuestaDB> ValidarToken(string email, string token)
        {
            try
            {
                var validacion = await _context.respuestaDB
                     .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Token @email, @token ", new SqlParameter("@email", email), new SqlParameter("@token", token))
                     .ToListAsync();

                if (validacion == null || !validacion.Any())
                {
                    return null;
                }

                return validacion.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<RespuestaDB> ActualizarContrasena(string contrasena, string email)
        {
            try
            {
                var Nuevotoken = Guid.NewGuid().ToString();

                var actualizacion = await _context.respuestaDB
                     .FromSqlRaw("EXEC dbo.sp_olvideLaContrasena_Actualizacion @contrasena, @email, @token ", 
                     new SqlParameter("@contrasena", contrasena), new SqlParameter("@email", email), new SqlParameter("@token", Nuevotoken))
                     .ToListAsync();

                if (actualizacion == null || !actualizacion.Any())
                {
                    return null;
                }

                return actualizacion.FirstOrDefault();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

    }
}
