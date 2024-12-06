
namespace RO.RentOfit.Infraestructure.Repositories
{
    public class AdministradorInfraestructure : IAdministradorInfraestructure
    {
        private readonly RentOutfitContext _context;
        private readonly EmailService _emailService;

        public AdministradorInfraestructure(RentOutfitContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;

        }

        public async Task<(List<ListaDeAprobacion> Establecimientos, int TotalRegistros)> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters)
        {
            int paginaValida = (parameters.Pagina == null || parameters.Pagina == 0) ? 1 : parameters.Pagina.Value;
            string orden = (parameters.Orden == null || parameters.Orden == "") ? "reciente" : parameters.Orden;

            try
            {
                var results = await _context.listaDeAprobacionsDto
                    .FromSqlRaw("EXEC sp_Mostrar_Peticiones_Establecimientos @usuario, @pagina, @filtro, @orden", 
                    new SqlParameter("usuario", parameters.Usuario), new SqlParameter("pagina", paginaValida), 
                    new SqlParameter("filtro", string.IsNullOrEmpty(parameters.Filtro) ? (object)DBNull.Value : parameters.Filtro),
                    new SqlParameter("orden", orden))
                    .ToListAsync();

                if (results.Count > 0) 
                {
                    int totalRegistros = results[0].totalRegistros;
                    return (results, totalRegistros);
                }

                return (new List<ListaDeAprobacion>(), 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar establecimientos para aprobación: " + ex.Message, ex);
            }
        }




        public async Task<RespuestaEstablecimiento> AprobarEstablecimiento(int establecimientoId)
        {
            try
            {
                var respuesta = await _context.respuestaEstablecimiento.FromSqlRaw("EXEC sp_Aprobar_Establecimiento @establecimiento ",
                new SqlParameter("@establecimiento", establecimientoId)).ToListAsync();

                if (respuesta == null || !respuesta.Any())
                {
                    return null;
                }

                string titulo = "¡Felicidades!¡Tu solitud fue aprobada!";
                string mensaje = $@"
                    <html>
                      <body style='font-family: Arial, sans-serif;  color: #ffffff; margin: 0; padding: 0; display: flex; justify-content: center;'>
                       <div style='background-color: #094e6d; padding: 60px; max-width: 420px; margin: auto; text-align: center; border-radius: 8px;'>
      
                           <div style='font-size: 30px; font-weight: bold; margin-bottom: 20px; color: #ffffff;'>¡Lo lograste!🎉</div>
        
                           <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 20px; text-align: justify;'>
                             Tras revisar tu solicitud, nos complace informate que, se ha aprobado tu establecimiento en nuestra plataforma. A partir
                             de ahora, tu establecimiento estará disponible para que los usuarios pueden verlo y explorar lo que tienes que ofrecer.
                          </div>

                          <img src='https://i.postimg.cc/mkqJrLzW/Prueba-removebg-preview.png' alt='Prueba1' style='width: 150px; margin-bottom: 20px; border-radius: 20px;' />

                          <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 40px; text-align: justify;'>
                            Gracias por confiar en nosotros. Si tienes alguna duda o necesitas ayuda adicional, no dudes en ponerte en contacto con nuestro
                            equipo de soporte.
                          </div>

                          <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 10px;'>
                            ¡Felicidades y mucho éxito con tu negocio!
                          </div>

                         <div style='font-size: 18px; line-height: 1.2; color: #ebebeb; margin-bottom: 20px;'>
                            Atentamente, RentOutfit
                         </div>

                         <div style='margin: 20px;'>
                            <a href='https://rent-outfit-web.vercel.app/Login' style='background-color: #ffffff; color: #000000; text-decoration: none; padding: 10px 20px;  border-radius: 10px; font-weight: bold;display: inline-block; width: 80%; max-width: 250px; text-align: center;'>Iniciar sesión</a>
                        </div>
                      </div>
                    </body>
                  </html>";

                var resultado = respuesta.FirstOrDefault();

                if (resultado != null)
                {
                    await _emailService.EnviarMsj(resultado.email, titulo, mensaje);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al actualizar la contraseña.", ex);
            }
        }


        public async Task<RespuestaEstablecimiento> DenegarEstablecimiento(MotivosDenegarAggregate requerimientos)
        {
            try
            {
                var respuesta = await _context.respuestaEstablecimiento.FromSqlRaw(
                   "EXEC sp_Denegar_Establecimiento @establecimiento ",
                   new SqlParameter("@establecimiento", requerimientos.establecimientoId)).ToListAsync();

                if (respuesta == null || !respuesta.Any())
                {
                    return null;
                }


                string titulo = "¡Importante! Tu solicitud ha sido rechazada";
                string mensaje = $@"
                <html>
                   <body style='font-family: Arial, sans-serif;  color: #ffffff; margin: 0; padding: 0; display: flex; justify-content: center;'>
                     <div style='background-color: #6d0909; padding: 60px; max-width: 420px; margin: auto; text-align: center; border-radius: 8px;'>
      
                        <div style='font-size: 30px; font-weight: bold; margin-bottom: 20px; color: #ffffff;'>Solicitud Rechazada</div>
        
                        <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 20px; text-align: justify;'>
                           Tras revisar cuidadosamente tu solicitud, lamentamos informarte que no cumple con algunos de los requisitos necesarios para ser publicada en nuestra plataforma.
                        </div>

                        <img src='https://i.postimg.cc/vHY6wTKC/denegar.png' alt='denegar' style='width: 150px; margin-bottom: 20px; border-radius: 20px;' />
       
                        <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 40px; text-align: justify;'>
                          <strong>Motivo: </strong> {requerimientos.motivo}
                        </div>

                        <div style='font-size: 20px; line-height: 1.4; color: #ebebeb; margin-bottom: 30px; text-align: justify;'>
                           Agradecemos tu interés en RentOutfit y te animamos a volver a intentarlo en el futuro.
                        </div>

                        <div style='font-size: 18px; line-height: 1.2; color: #ebebeb;'>
                           Atentamente, RentOutfit
                        </div>

                      </div>
                   </body>
                </html>
                ";

                var resultado = respuesta.FirstOrDefault();

                if (resultado != null)
                {
                    await _emailService.EnviarMsj(resultado.email, titulo, mensaje);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al denegar .", ex);
            }
        } 





        public async Task<List<EstablecimientosCercanosDto>> TodosLosEstablecimientos(TodosEstablecimientosAggregate requerimientos) 
        {
            try 
            {
                int paginaValida = (requerimientos.pagina == null || requerimientos.pagina == 0) ? 1 : requerimientos.pagina.Value;

                var respuesta = await _context.establecimientosCercanosDto.FromSqlRaw(
                    "EXEC sp_Todos_Los_Establecimientos @usuario, @pagina ", new SqlParameter("@usuario", requerimientos.usuario), new SqlParameter("@pagina", paginaValida)
                    ).ToListAsync();

                if (respuesta == null || !respuesta.Any()) 
                {
                    return null;
                }

                return respuesta.ToList();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al denegar .", ex);
            }
        }



        public async Task Alertar(MandarMsj requerimientos)
        {
            try
            {
                int pagina = 1;
                bool activar = false;

                var respuesta = await _context.clienteDto
                   .FromSqlRaw("EXEC dbo.sp_mostrar_cliente @usuarioID, @pagina, @activar ",
                    new SqlParameter("usuarioID", requerimientos.usuarioID), new SqlParameter("pagina", pagina),
                    new SqlParameter("activar", activar)).ToListAsync();

                var resultado = respuesta.FirstOrDefault();

                string titulo = "Aviso importante!!";
                string mensaje = $@"{requerimientos.mensaje}";

                if (resultado != null)
                {
                    await _emailService.EnviarMsj(resultado.email, titulo, mensaje);
                }

            }
            catch (Exception ex) 
            {
                throw new Exception("Error al alertar al usuario ", ex);
            }
        }



    }
}
