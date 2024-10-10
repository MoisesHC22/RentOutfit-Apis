
using RO.RentOfit.Domain.DTOs.Cliente;

namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructure
    {
        Task<IniciarSesionDto> IniciarSesion(RequerimientoIniciarSesionDto requerimiento);
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteDto registro);
    }
}
