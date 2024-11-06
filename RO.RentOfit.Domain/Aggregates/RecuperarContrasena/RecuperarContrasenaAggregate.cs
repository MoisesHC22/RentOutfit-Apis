
namespace RO.RentOfit.Domain.Aggregates.RecuperarContrasena
{
    public class RecuperarContrasenaAggregate
    {
        public string? email { get; set; }
        public string? token { get; set; }
        public string? contrasena { get; set; }
    }

    public class RequerimientosCorreoAggregate
    {
        public string? email { get; set; }
    }
}
