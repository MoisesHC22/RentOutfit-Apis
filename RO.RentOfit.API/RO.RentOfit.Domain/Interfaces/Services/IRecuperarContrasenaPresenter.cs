
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IRecuperarContrasenaPresenter
    {
        Task<RespuestaDB> ActualizarContrasena(string contrasena, string email);
        Task<RecuperarContrasenaDto> ObtenerToken(string email);
        Task<RespuestaDB> ValidarToken(string email, string token);
    }
}
