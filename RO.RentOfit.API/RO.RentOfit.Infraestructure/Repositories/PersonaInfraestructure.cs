/// Developer : Nombre desarrollador
/// Creation Date : 25/09/2024
/// Creation Description: Class for connection with the DB
/// Update Date : --
/// Update Description : --
///CopyRight: Nombre proyecto
namespace RO.RentOfit.Infraestructure.Repositories;
public class PersonaInfraestructure : IPersonaInfraestructure
{
    private readonly RentOutfitContext _context;
    public PersonaInfraestructure(RentOutfitContext context)
    {
        _context = context;
    }


}
