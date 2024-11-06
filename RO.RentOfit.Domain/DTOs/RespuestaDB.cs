/// Developer : Nombre desarrollador
/// Creation Date : 25/09/2024
/// Creation Description:Dto class
/// Update Date : --
/// Update Description : --
///CopyRight: Nombre proyecto
namespace RO.RentOfit.Domain.DTOs
{
    public class RespuestaDB
    {
        [Key]
        public int tipoError {  get; set; }
        public string mensaje {  get; set; }
    }

    public class RespuestaEstablecimiento
    {
        [Key]
        public int tipoError { get; set; }
        public string mensaje { get; set; }
        public string? email { get; set; }
    }

    public class RespuestaValidarToken
    {
        [Key]
        public int tipoError { get; set; }
        public string mensaje { get; set; }
        public string? imagen { get; set; }
    }
}
