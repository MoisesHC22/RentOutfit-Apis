namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class ClienteInfraestructure : IClienteInfraestructure
    {
        private readonly RentOutfitContext _context;
        private readonly StorageFirebaseConfig _storageFirebase;
        private readonly EmailService _emailService;

        public ClienteInfraestructure(RentOutfitContext context, StorageFirebaseConfig storageFirebase, EmailService emailService)
        {
            _context = context;
            _storageFirebase = storageFirebase;
            _emailService = emailService;
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
                  new SqlParameter("pagina", paginaValida),
                  new SqlParameter("filtro", string.IsNullOrEmpty(requerimientos.filtro) ? (object)DBNull.Value : requerimientos.filtro)
                };

                var sqlQuery = "EXEC dbo.sp_consultar_vestimentas_Establecimiento @establecimiento, @usuario, @pagina, @filtro";
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


        public async Task<InformacionDeUnaVestimenta> InformacionDeUnaSolaVestimenta(int vestimentaID) 
        {
            try 
            {
               var vestimenta = await _context.informacionDeUnaVestimenta
                   .FromSqlRaw("EXEC dbo.sp_Buscar_Inf_Vestimenta @vestimentaID ", new SqlParameter("@vestimentaID", vestimentaID))
                   .ToListAsync();

                return vestimenta.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar un establecimiento, ", ex);
            }
        }


        public async Task<ClienteDto> PagoExitoso(int usuarioID) 
        {
            try
            {
                var carrito = await CargarCarrito(usuarioID);
                
                if (carrito == null && carrito.Any())
                { 
                    throw new Exception("El carrito está vacío o no pudo ser cargado.");
                }


                decimal total = 0;

                foreach (var item in carrito)
                {
                    var vestimenta = await InformacionDeUnaSolaVestimenta(item.vestimentaID);

                    if (vestimenta != null)
                    {
                      total += vestimenta.precioPorDia * item.stock;
                    }
                }

                var pedido = await _context.pedidoDto.FromSqlRaw("EXEC dbo.sp_Generar_Pedido @usuarioID, @total, @estatus ",
                    new SqlParameter("@usuarioID", usuarioID), new SqlParameter("@total", total), new SqlParameter("@estatus", 1))
                    .ToListAsync();

                var respuestaPedido = pedido.FirstOrDefault();

                if (respuestaPedido == null || respuestaPedido.pedidoID == null)
                {
                    throw new Exception("No se pudo generar el pedido.");
                }

                foreach (var item in carrito)
                {
                    var vestimenta = await InformacionDeUnaSolaVestimenta(item.vestimentaID);

                    if (vestimenta != null)
                    {
                        var detalleVenta = await _context.respuestaDB.FromSqlRaw("EXEC dbo.sp_Generar_DetalleVenta @pedidoID, @vestimentaID, @precioTotal, @cantidad, @fechaPrestamo ",
                            new SqlParameter("@pedidoID", respuestaPedido.pedidoID), new SqlParameter("@vestimentaID", item.vestimentaID),
                            new SqlParameter("@precioTotal", vestimenta.precioPorDia * item.stock), new SqlParameter("@cantidad", item.stock), new SqlParameter("@fechaPrestamo", item.fechaPrestamo)).ToListAsync();
                    }

                }

                var requerimientos = new CarritoAggregate
                {
                    usuarioID = usuarioID,
                    itemsCarrito = new List<ItemsCarrito>()
                };

                await _storageFirebase.CarritoCompras(requerimientos);


                var cliente = await _context.clienteDto.FromSqlRaw("EXEC dbo.sp_mostrar_cliente @usuarioID, @pagina, @activar ",
                    new SqlParameter("@usuarioID", usuarioID), new SqlParameter("@pagina", 1), new SqlParameter("@activar", false))
                    .ToListAsync();

                var resultado = cliente.FirstOrDefault();


                if (resultado != null) 
                {
                  string titulo = "¡Pago con exito!";
                  string mensaje = $@"
                
                   <html>
                    <body style='font-family: Arial, sans-serif;  color: #ffffff; margin: 0; padding: 0; display: flex; justify-content: center;'>
                      <div style='background-color: #094e6d; padding: 60px; max-width: 420px; margin: auto; text-align: center; border-radius: 8px;'>
      
                       <div style='font-size: 30px; font-weight: bold; margin-bottom: 20px; color: #ffffff;'>¡Pago Exitoso!🎉</div>
        
                       <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 40px; text-align: justify;'>
                         ¡Muchas gracias {resultado.nombreCliente + " " + resultado.apellidoPaterno + " " + resultado.apellidoMaterno} por eligirnos! Nos emociona saber que has confiado en nosotros para lucir increíble en esa ocasión tan importante. 👗🤵✨
                       </div>

                       <img src='https://i.postimg.cc/mkqJrLzW/Prueba-removebg-preview.png' alt='Prueba1' style='width: 150px; margin-bottom: 20px; border-radius: 20px;' />

                       <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 10px;'>
                         ¡Disfruta al máximo tu momento y luce como nunca! Gracias por permitirnos ser parte de esta experiencia. ❤️
                       </div>

                       <div style='margin: 20px;'>
                         <a href='https://rent-outfit-web.vercel.app/Login' style='background-color: #ffffff; color: #000000; text-decoration: none; padding: 10px 20px;  border-radius: 10px; font-weight: bold;display: inline-block; width: 80%; max-width: 250px; text-align: center;'>Ver mis compras</a>
                       </div>
                     </div>
                    </body>
                   </html>";

                   await _emailService.EnviarMsj(resultado.email, titulo, mensaje);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente.", ex);
            }
        }

        public async Task<List<ListaPedido>> ListaDeRentas(ListaDePedidoAggregate requerimientos)
        {
            try
            {
                int paginaValida = (requerimientos.pagina == null || requerimientos.pagina == 0) ? 1 : requerimientos.pagina.Value;

                var pedidos = await _context.listaPedido
                    .FromSqlRaw("EXEC dbo.sp_Lista_De_Pedidos @usuarioID, @mes, @ano, @pagina ", 
                    new SqlParameter("@usuarioID", requerimientos.usuarioID), new SqlParameter("@mes", requerimientos.mes), 
                    new SqlParameter("@ano", requerimientos.ano), new SqlParameter("@pagina", paginaValida))
                    .ToListAsync();

                if (pedidos == null || !pedidos.Any())
                {
                    return null;
                }
                return pedidos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar mis compras, ", ex);
            }
        } 


    }
}