
namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class ClienteInfraestructure : IClienteInfraestructure
    {
        private readonly RentOutfitContext _context;
        public ClienteInfraestructure(RentOutfitContext context) 
        {
            _context = context;
        }

        public async Task<List<ClienteDto>> ObtenerCliente(int usuarioID)
        {
            try
            {
                var clientes = await _context.clienteDto
                    .FromSqlRaw("EXEC sp_mostrar_cliente @usuarioID ", new SqlParameter("@usuarioID", usuarioID))
                    .ToListAsync();

                return (clientes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente.", ex);
            }
        }


        public async Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("email", registro.email),
                    new SqlParameter("contrasena", registro.controsena),
                    new SqlParameter("token", registro.token),
                    new SqlParameter("tokenValidacion", registro.tokenValidacion),
                    new SqlParameter("nombreCliente", registro.nombreCliente),
                    new SqlParameter("apellidoPaterno", registro.apellidoPaterno),
                    new SqlParameter("apellidoMaterno", registro.apellidoMaterno),
                    new SqlParameter("linkImagenPerfil", registro.linkImagenPerfil),
                    new SqlParameter("telefono", registro.telefono),
                    new SqlParameter("generoID", registro.generoID),
                    new SqlParameter("codigoPostal", registro.codigoPostal),
                    new SqlParameter("colonia", registro.colonia),
                    new SqlParameter("calle", registro.calle),
                    new SqlParameter("noInt", registro.noInt),
                    new SqlParameter("noExt", registro.noExt),
                    new SqlParameter("estadoID", registro.estadoID),
                    new SqlParameter("municipio", registro.municipio)
                };

                var sqlQuery = "EXEC dbo.sp_registrar_cliente @email, @contrasena, @token, @tokenValidacion, " +
                    "@nombreCliente, @apellidoPaterno, @apellidoMaterno, @linkImagenPerfil, @telefono, @generoID, " +
                    "@codigoPostal, @colonia, @calle, @noInt, @noExt, @estadoID, @municipio";

                var dataSP = await _context.respuestaDB.FromSqlRaw(sqlQuery, parameters).ToListAsync();

                return dataSP.FirstOrDefault();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }



        public async Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento)
        {
            try
            {
                var clientes = await _context.iniciarSesionDto
                  .FromSqlRaw("EXEC dbo.sp_Iniciar_Sesion @email, @contrasena ", new SqlParameter("@email", requerimiento.email), new SqlParameter("@contrasena", requerimiento.contrasena))
                  .ToListAsync();

                if (clientes == null || !clientes.Any())
                {
                    return null;
                }

                return clientes.FirstOrDefault();

            }
            catch (Exception ex) 
            {
                throw new Exception("Error al intentar iniciar sesión.");
            }
        }





    }
}
