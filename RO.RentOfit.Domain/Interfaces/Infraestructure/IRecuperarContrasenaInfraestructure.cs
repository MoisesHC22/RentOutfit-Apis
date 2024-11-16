
namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IRecuperarContrasenaInfraestructure
    {
        Task<RespuestaDB> ActualizarContrasena(ActualizarContrasena Requerimientos);
        Task<RecuperarContrasenaDto> ObtenerToken(string email);
        Task<RespuestaValidarToken> ValidarToken(ValidarToken requerimientos);
    }
}
