/// Developer : Nombre desarrollador
/// Creation Date : 25/09/2024
/// Creation Description:Interface
/// Update Date : --
/// Update Description : --
///CopyRight: Nombre proyecto
namespace RO.RentOfit.Domain.Interfaces.Infraestructure;
public interface IPersonaInfraestructure
{
    /// <summary>
    /// Consulta un registro de la tabla GI_Persona
    /// </summary>
    /// <returns></returns>
    Task<PersonaDto> GetPersona();

    /// <summary>
    /// Agrega un registro de la tabla GI_Persona
    /// </summary>
    /// <returns></returns>
    Task<RespuestaDB> AddPersona(PersonaAggregate aggregate);
}
