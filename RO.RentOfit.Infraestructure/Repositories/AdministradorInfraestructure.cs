using System.Collections.Generic;
using System.Threading.Tasks;
using RO.RentOfit.Domain.DTOs.Establecimientos;
using RO.RentOfit.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace RO.RentOfit.Infraestructure.Repositories
{
    public class AdministradorInfraestructure : IAdministradorInfraestructure
    {
        private readonly RentOutfitContext _context;

        public AdministradorInfraestructure(RentOutfitContext context)
        {
            _context = context;
        }

        public async Task<List<ListaDeAprobacion>> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters)
        {
            int paginaValida = (parameters.Pagina == null || parameters.Pagina == 0) ? 1 : parameters.Pagina.Value;

            try
            {
                var results = await _context.listaDeAprobacionsDto
                    .FromSqlRaw("EXEC sp_Mostrar_Peticiones_Establecimientos @usuario, @pagina", new SqlParameter("usuario", parameters.Usuario), new SqlParameter("pagina", paginaValida))
                    .ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar establecimientos para aprobación: " + ex.Message, ex);
            }
        }

        public async Task<RespuestaDB> AprobarEstablecimiento(int establecimientoId)
        {
            try { 
            var respuesta = await _context.respuestaDB.FromSqlRaw(
                "EXEC sp_Aprobar_Establecimiento @establecimiento ", new SqlParameter("@establecimiento", establecimientoId)
                ).ToListAsync();
            if (respuesta == null || !respuesta.Any())
        {
            return null;
        }

             return respuesta.FirstOrDefault();
        }
            catch (Exception ex)
     {
        // Manejar la excepción y registrarla si es necesario
        throw new Exception("Error al actualizar la contraseña.", ex);
    }
}

        public async Task<RespuestaDB>DenegarEstablecimiento(int establecimientoId)
        {
            try
            {
                var respuesta = await _context.respuestaDB.FromSqlRaw(
                   "EXEC sp_Denegar_Establecimiento @establecimiento ",new SqlParameter("@establecimiento", establecimientoId)
                   ).ToListAsync();
                if (respuesta == null || !respuesta.Any())
                {
                    return null;
                }

                return respuesta.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrarla si es necesario
                throw new Exception("Error al denegar .", ex);
            }
        }

        public async Task<List<InformacionEstablecimientoDto>> ListarEstablecimientosPendientes()
        {
            return await _context.Set<InformacionEstablecimientoDto>()
                .FromSqlRaw("EXEC sp_ListarEstablecimientosPendientes")
                .ToListAsync();
        }
    }
}
