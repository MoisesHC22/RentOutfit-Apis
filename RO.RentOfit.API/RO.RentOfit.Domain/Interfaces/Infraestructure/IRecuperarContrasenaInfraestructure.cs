
namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IRecuperarContrasenaInfraestructure
    {
        Task<RespuestaDB> ActualizarContrasena(string contrasena, string email);
        Task<RecuperarContrasenaDto> ObtenerToken(string email);
        Task<RespuestaDB> ValidarToken(string email, string token);
    }
}
