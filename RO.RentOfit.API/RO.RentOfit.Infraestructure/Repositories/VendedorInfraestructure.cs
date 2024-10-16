
namespace RO.RentOfit.Infraestructure.Repositories
{
    internal class VendedorInfraestructure : IVendedorInfraestructure
    { 
        private readonly RentOutfitContext _context;
        private readonly StorageFirebaseConfig _storageFirebase;

        public VendedorInfraestructure(RentOutfitContext context, StorageFirebaseConfig storageFirebase)
        {
            _context = context;
            _storageFirebase = storageFirebase;
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



        public async Task<RespuestaDB> RegistrarVestimentas(VestimentaAggregate registro, IFormFile[] Imagenes)
        {
            try
            {
                var ubicacion = "Producto/" + registro.usuarioID + "_" + registro.nombre + "/";

                string[] linksImagenes = new string[4];

                for (int i = 0; i < Imagenes.Length; i++)
                {

                    if (Imagenes[i] != null)
                    {
                        var nombreImg = registro.nombre + "_img" + (i + 1);
                        linksImagenes[i] = await _storageFirebase.SubirArchivo(Imagenes[i], nombreImg, ubicacion);
                    }
                    else 
                    {
                        linksImagenes[i] = null;
                    }
                }


                SqlParameter[] parameters =
                {
                new SqlParameter("usuarioID", registro.usuarioID),
                new SqlParameter("nombre", registro.nombre),
                new SqlParameter("precio", registro.precio),
                new SqlParameter("stock", registro.stock),
                new SqlParameter("tallaID", registro.tallaID),
                new SqlParameter("estiloID", registro.estiloID),
                new SqlParameter("descripcion", registro.descripcion),
                new SqlParameter("imagen1", linksImagenes[0]),
                new SqlParameter("imagen2", (object)linksImagenes[1] ?? DBNull.Value),
                new SqlParameter("imagen3", (object)linksImagenes[2] ?? DBNull.Value),
                new SqlParameter("imagen4", (object)linksImagenes[3] ?? DBNull.Value)
                };

                var sqlQuery = "EXEC dbo.sp_registrar_vestimenta @usuarioID, @nombre, @precio, " +
                    "@stock, @tallaID, @estiloID, @descripcion, @imagen1, @imagen2, @imagen3, @imagen4";

                var dataSP = await _context.respuestaDB.FromSqlRaw(sqlQuery, parameters).ToListAsync();

                return (dataSP.FirstOrDefault());
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al registrar la vestimenta con imágenes.", ex);
            }
        }




    }
}
