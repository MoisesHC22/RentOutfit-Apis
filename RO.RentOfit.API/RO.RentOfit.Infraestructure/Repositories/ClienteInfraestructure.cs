namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class ClienteInfraestructure : IClienteInfraestructure
    {
        private readonly RentOutfitContext _context;
        private readonly StorageFirebaseConfig _storageFirebase;

        public ClienteInfraestructure(RentOutfitContext context, StorageFirebaseConfig storageFirebase)
        {
            _context = context;
            _storageFirebase = storageFirebase;
        }

        // Método para obtener un cliente
        public async Task<List<ClienteDto>> ObtenerCliente(int usuarioID)
        {
            try
            {
                var clientes = await _context.clienteDto
                    .FromSqlRaw("EXEC dbo.sp_mostrar_cliente @usuarioID ", new SqlParameter("@usuarioID", usuarioID))
                    .ToListAsync();

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente.", ex);
            }
        }

        // Método original para registrar un cliente
        public async Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro)
        {
            try
            {
                var token = Guid.NewGuid().ToString();

                SqlParameter[] parameters =
                {
                    new SqlParameter("email", registro.email),
                    new SqlParameter("contrasena", registro.contrasena),
                    new SqlParameter("token", token),
                    new SqlParameter("nombreCliente", registro.nombreCliente),
                    new SqlParameter("apellidoPaterno", registro.apellidoPaterno),
                    new SqlParameter("apellidoMaterno", registro.apellidoMaterno),
                    new SqlParameter("linkImagenPerfil", DBNull.Value),
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

                var sqlQuery = "EXEC dbo.sp_registrar_cliente @email, @contrasena, @token, " +
                               "@nombreCliente, @apellidoPaterno, @apellidoMaterno, @linkImagenPerfil, @telefono, @generoID, " +
                               "@codigoPostal, @colonia, @calle, @noInt, @noExt, @estadoID, @municipio";

                var dataSP = await _context.respuestaDB.FromSqlRaw(sqlQuery, parameters).ToListAsync();
                var respuesta = dataSP.FirstOrDefault();

                if (respuesta != null && registro.imagen != null)
                {
                    var ubicacion = "perfiles/";
                    var nombreImg = registro.nombreCliente + "_" + registro.email;
                    var linkImg = await _storageFirebase.SubirArchivo(registro.imagen, nombreImg, ubicacion);

                    var actualizacion = await _context.respuestaDB
                        .FromSqlRaw("EXEC dbo.sp_actualizar_fotoDePefil @email, @linkImagenPerfil",
                                    new SqlParameter("@email", registro.email),
                                    new SqlParameter("@linkImagenPerfil", linkImg))
                        .ToListAsync();
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Método para registrar un cliente usando SqlCommand
        public async Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro, IFormFile Imagen)
        {
            try
            {
                var token = Guid.NewGuid().ToString();

                // Usamos SqlConnection directamente
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    // Usamos SqlCommand para ejecutar el procedimiento almacenado
                    using (var command = new SqlCommand("sp_registrar_cliente", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar los parámetros al SqlCommand
                        command.Parameters.AddWithValue("@email", registro.email);
                        command.Parameters.AddWithValue("@contrasena", registro.contrasena);
                        command.Parameters.AddWithValue("@token", token);
                        command.Parameters.AddWithValue("@nombreCliente", registro.nombreCliente);
                        command.Parameters.AddWithValue("@apellidoPaterno", registro.apellidoPaterno);
                        command.Parameters.AddWithValue("@apellidoMaterno", registro.apellidoMaterno);
                        command.Parameters.AddWithValue("@linkImagenPerfil", DBNull.Value);
                        command.Parameters.AddWithValue("@telefono", registro.telefono);
                        command.Parameters.AddWithValue("@generoID", registro.generoID);
                        command.Parameters.AddWithValue("@codigoPostal", registro.codigoPostal);
                        command.Parameters.AddWithValue("@colonia", registro.colonia);
                        command.Parameters.AddWithValue("@calle", registro.calle);
                        command.Parameters.AddWithValue("@noInt", registro.noInt);
                        command.Parameters.AddWithValue("@noExt", registro.noExt);
                        command.Parameters.AddWithValue("@estadoID", registro.estadoID);
                        command.Parameters.AddWithValue("@municipio", registro.municipio);

                        // Ejecutar el comando y obtener el resultado
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                // Leer los resultados de la consulta
                                await reader.ReadAsync();
                                var respuesta = new RespuestaDB
                                {
                                    tipoError = reader.GetInt32(0),
                                    mensaje = reader.GetString(1)
                                };

                                if (respuesta.tipoError == 0)
                                {
                                    var ubicacion = "perfiles/";
                                    var nombreImg = registro.nombreCliente + "_" + registro.email;
                                    var linkImg = await _storageFirebase.SubirArchivo(Imagen, nombreImg, ubicacion);

                                    // Actualizar la imagen del perfil después de haber registrado el cliente
                                    using (var updateCommand = new SqlCommand("sp_actualizar_fotoDePefil", connection))
                                    {
                                        updateCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                        updateCommand.Parameters.AddWithValue("@email", registro.email);
                                        updateCommand.Parameters.AddWithValue("@linkImagenPerfil", linkImg);
                                        await updateCommand.ExecuteNonQueryAsync();
                                    }
                                }

                                return respuesta;
                            }
                            else
                            {
                                throw new Exception("No se pudo registrar el cliente.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar cliente.", ex);
            }
        }

        // Método para iniciar sesión
        public async Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento)
        {
            try
            {
                var clientes = await _context.iniciarSesionDto
                  .FromSqlRaw("EXEC dbo.sp_Iniciar_Sesion @email, @contrasena ",
                  new SqlParameter("@email", requerimiento.email),
                  new SqlParameter("@contrasena", requerimiento.contrasena))
                  .ToListAsync();

                if (clientes == null || !clientes.Any())
                {
                    return null;
                }

                return clientes.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar iniciar sesión.", ex);
            }
        }
    }
}
