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

        public async Task<List<ClienteDto>> ObtenerCliente(ObtenerClientesAggregate requerimientos)
        {
            try
            {
                int paginaValida = (requerimientos.pagina == null || requerimientos.pagina == 0) ? 1 : requerimientos.pagina.Value;
                bool activarValida = (requerimientos.activar == null) ? false : requerimientos.activar.Value;

                SqlParameter[] parameters =
                {
                    new SqlParameter("usuarioID", requerimientos.usuarioID),
                    new SqlParameter("pagina", paginaValida),
                    new SqlParameter("activar", activarValida),
                };


                var sqlQuery = "EXEC dbo.sp_mostrar_cliente @usuarioID, @pagina, @activar ";
                var cliente = await _context.clienteDto.FromSqlRaw(sqlQuery, parameters).ToListAsync();

                return cliente.ToList();
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
                    new SqlParameter("generoID", registro.generoID)

                };

                var sqlQuery = "EXEC dbo.sp_registrar_cliente @email, @contrasena, @token, " +
                               "@nombreCliente, @apellidoPaterno, @apellidoMaterno, @linkImagenPerfil, @telefono, @generoID";

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


        public async Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta)
        {
            try
            {
                var info = await _context.informacionVestimentaDto
                    .FromSqlRaw("EXEC dbo.sp_Informacion_Vestimenta @vestimenta ",
                    new SqlParameter("@vestimenta", vestimenta))
                    .ToListAsync();

                if (info == null || !info.Any())
                {
                    return null;
                }
                return info.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar información.", ex);
            }
        }


        public async Task<List<EstablecimientosCercanosDto>> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos)
        {
            try
            {

                int paginaValida = (requerimientos.pagina == null || requerimientos.pagina == 0) ? 1 : requerimientos.pagina.Value;

                SqlParameter[] parameters =
                {
                  new SqlParameter("estado", requerimientos.estado),
                  new SqlParameter("municipio", requerimientos.municipio),
                  new SqlParameter("pagina", paginaValida)
                };

                var sqlQuery = "EXEC dbo.sp_Establecimientos_Cercanos @estado, @municipio, @pagina ";
                var establecimiento = await _context.establecimientosCercanosDto.FromSqlRaw(sqlQuery, parameters).ToListAsync();


                if (establecimiento == null || !establecimiento.Any())
                {
                    return null;
                }

                return establecimiento.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar un establecimiento, ", ex);
            }
        }


        public async Task<List<ListaVestimentasDto>> MostrarVestimentas(RequisitosVestimentaAggregate requisitos)
        {
            try
            {
                int paginaValida = (requisitos.pagina == null || requisitos.pagina == 0) ? 1 : requisitos.pagina.Value;

                SqlParameter[] parameters =
                {
                  new SqlParameter("estado", requisitos.estado),
                  new SqlParameter("municipio", requisitos.municipio),
                  new SqlParameter("pagina", paginaValida),
                  new SqlParameter("filtro",  string.IsNullOrEmpty(requisitos.filtro) ? (object)DBNull.Value : requisitos.filtro),
                  new SqlParameter("categoria", requisitos.categoria == null || requisitos.categoria == 0 ? (object)DBNull.Value : requisitos.categoria ),
                   new SqlParameter("talla", requisitos.talla == null || requisitos.talla == 0 ? (object)DBNull.Value : requisitos.talla )
                };

                var sqlQuery = "EXEC dbo.sp_mostrar_vestimenta @estado, @municipio, @pagina, @filtro, @categoria, @talla ";
                var establecimiento = await _context.listaVestimentasDto.FromSqlRaw(sqlQuery, parameters).ToListAsync();


                if (establecimiento == null || !establecimiento.Any())
                {
                    return null;
                }

                return establecimiento.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener vestimentas.", ex);
            }
        }


        public async Task<InformacionEstablecimientoDto> InformacionEstablecimiento(int establecimiento)
        {
            try
            {
                var info = await _context.informacionEstablecimientoDto
                    .FromSqlRaw("EXEC dbo.sp_Informacion_Establecimiento @establecimiento ",
                    new SqlParameter("@establecimiento", establecimiento))
                    .ToListAsync();

                if (info == null || !info.Any())
                {
                    return null;
                }

                return info.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la informacion del establecimiento. ", ex);
            }
        }


        public async Task<List<VestimentasEstablecimientosDto>> VestimentasDeEstablecimientos(VestimentasEstablecimientosAggregate requerimientos)
        {
            try
            {

                int paginaValida = (requerimientos.pagina == null || requerimientos.pagina == 0) ? 1 : requerimientos.pagina.Value;

                SqlParameter[] parameters =
                {
                  new SqlParameter("establecimiento", requerimientos.establecimiento),
                  new SqlParameter("usuario", requerimientos.usuario),
                  new SqlParameter("pagina", paginaValida)
                };

                var sqlQuery = "EXEC dbo.sp_consultar_vestimentas_Establecimiento @establecimiento, @usuario, @pagina ";
                var establecimiento = await _context.vestimentasEstablecimientosDto.FromSqlRaw(sqlQuery, parameters).ToListAsync();


                if (establecimiento == null || !establecimiento.Any())
                {
                    return null;
                }

                return establecimiento.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar un establecimiento, ", ex);
            }
        }

        public async Task GuardarCarrito(CarritoAggregate requerimientos) 
        { 
            await _storageFirebase.CarritoCompras(requerimientos);
        }

        public async Task<List<ItemsCarrito>> CargarCarrito(int usuarioID) 
        {
           return await _storageFirebase.ObtenerCarritoCompras(usuarioID);
        }


    }
}