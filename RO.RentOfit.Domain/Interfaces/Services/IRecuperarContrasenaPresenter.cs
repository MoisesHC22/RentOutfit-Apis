
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IRecuperarContrasenaPresenter
    {
        Task<RespuestaDB> ActualizarContrasena(ActualizarContrasena Requerimientos);
        Task<RecuperarContrasenaDto> ObtenerToken(string email);
        Task<RespuestaValidarToken> ValidarToken(ValidarToken requerimientos);
    }
}
