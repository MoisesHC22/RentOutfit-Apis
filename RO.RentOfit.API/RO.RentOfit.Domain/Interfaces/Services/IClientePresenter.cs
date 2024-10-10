
using RO.RentOfit.Domain.DTOs.Cliente;

namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IClientePresenter
    {
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);

        Task<RespuestaDB> RegistrarCliente(RegistrarClienteDto registro);
    }
}
