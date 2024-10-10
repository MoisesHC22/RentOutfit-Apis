
using RO.RentOfit.Domain.DTOs.Cliente;

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

    }
}
