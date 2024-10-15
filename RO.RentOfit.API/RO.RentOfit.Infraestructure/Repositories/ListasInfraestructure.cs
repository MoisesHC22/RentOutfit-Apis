
namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class ListasInfraestructure : IListasInfraestructure
    {
        private readonly RentOutfitContext _context;

        public ListasInfraestructure(RentOutfitContext context) 
        {
            _context = context;
        }

        public async Task<List<GeneroDto>> ObtenerGeneros()
        {
            try
            {
                var generos = await _context.generoDto
                    .FromSqlRaw("EXEC dbo.sp_consultar_generos")
                    .ToListAsync();

                return (generos);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<List<EstadosDto>> ObtenerEstados()
        {
            try
            {
                var estados = await _context.estadosDto
                    .FromSqlRaw("EXEC dbo.sp_consultar_estados")
                    .ToListAsync();

                return (estados);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<MunicipiosDto>> ObtenerMunicipios(int estadoID)
        {
            try
            {
                var municipios = await _context.municipiosDto
                    .FromSqlRaw("EXEC sp_consultar_municipios @estadoID ", new SqlParameter("@estadoId", estadoID))
                    .ToListAsync();

                return (municipios);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TallasDto>> ObtenerTallas()
        {
            try
            {
                var tallas = await _context.tallasDto
                    .FromSqlRaw("EXEC dbo.sp_consultar_tallas")
                    .ToListAsync();

                return (tallas);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EstilosDto>> ObtenerEstilos()
        {
            try
            {
                var estilos = await _context.estilosDto
                    .FromSqlRaw("EXEC dbo.sp_consultar_estilos")
                    .ToListAsync();

                return (estilos);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }


    }
}
