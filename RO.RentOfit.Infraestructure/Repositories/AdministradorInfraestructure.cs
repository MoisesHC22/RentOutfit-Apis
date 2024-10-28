
namespace RO.RentOfit.Infraestructure.Repositories
{
    public class AdministradorInfraestructure : IAdministradorInfraestructure
    {
        private readonly RentOutfitContext _context;

        public AdministradorInfraestructure(RentOutfitContext context) 
        {
            _context = context;
        }





    }
}
