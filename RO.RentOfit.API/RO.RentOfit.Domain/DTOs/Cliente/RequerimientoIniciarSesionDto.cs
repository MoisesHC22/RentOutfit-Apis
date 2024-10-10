
namespace RO.RentOfit.Domain.DTOs.Cliente
{
    public class RequerimientoIniciarSesionDto
    {
        [Key]
        public string email { get; set; }
        public string contrasena { get; set; }
    }
}
