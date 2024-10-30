
using RO.RentOfit.Domain.Aggregates.RecuperarContrasena;

namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IRecuperarContrasenaPresenter
    {
        Task<RespuestaDB> ActualizarContrasena(ActualizarContrasena Requerimientos);
        Task<RecuperarContrasenaDto> ObtenerToken(string email);
        Task<RespuestaDB> ValidarToken(ValidarToken requerimientos);
    }
}
