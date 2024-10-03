namespace RO.RentOfit.Domain.DTOs.Persona;
public class PersonaDto
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
}
