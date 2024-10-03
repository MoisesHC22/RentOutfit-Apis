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
        public int TipoError {  get; set; }
        public string Mensaje {  get; set; }
    }
}
