
namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class VendedorInfraestructure : IVendedorInfraestructure
    { 
        private readonly RentOutfitContext _context;

        public VendedorInfraestructure(RentOutfitContext context)
        {
            _context = context;
        }

        public async Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID)
        {
            try
            {
                var nuevoVendedor = await _context.respuestaDB
                    .FromSqlRaw("EXEC dbo.sp_DardeAlta_Vendedor @usuarioID ", new SqlParameter("@usuarioID", usuarioID))
                    .ToListAsync();

                return nuevoVendedor.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dar de alta un vendedor.");
            }
        }



        public async Task<RespuestaDB> DarDeAltaEstablecimiento(EstablecimientoAggregate registro)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("usuarioID", registro.usuarioID),
                new SqlParameter("nombreEstablecimiento", registro.nombreEstablecimiento),
                new SqlParameter("codigoPostal", registro.codigoPostal),
                new SqlParameter("colonia", registro.colonia),
                new SqlParameter("calle", registro.calle),
                new SqlParameter("noInt", registro.noInt),
                new SqlParameter("noExt", registro.noExt),
                new SqlParameter("estadoID", registro.estadoID),
                new SqlParameter("municipio", registro.municipio)
            };

            var sqlQuery = "EXEC dbo.sp_DarDeAlta_Establecimiento @usuarioID, @nombreEstablecimiento, @codigoPostal, " +
                "@colonia, @calle, @noInt, @noExt, @estadoID, @municipio";

            var dataSP = await _context.respuestaDB.FromSqlRaw(sqlQuery, parameters).ToListAsync();

            return (dataSP.FirstOrDefault());

        }




    }
}
