
namespace RO.RentOfit.Domain.Aggregates.RecuperarContrasena
{
    public class ValidarToken
    {
        public string email { get; set; }
        public string token { get; set; }
    }

    public class ActualizarContrasena
    {
        public string contrasena { get; set; }
        public string email { get; set; }
    }
}
