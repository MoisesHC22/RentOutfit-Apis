/// Developer : Nombre desarrollador
/// Creation Date : 25/09/2024
/// Creation Description:Interface
/// Update Date : --
/// Update Description : --
///CopyRight: Nombre proyecto
namespace RO.RentOfit.Aplication.Presenters
{
    public class PersonaPresenter : IPersonaPresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public PersonaPresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

    }
}
