
namespace RO.RentOfit.Aplication.Presenters
{
    public class AdminstradorPresenter : IAdministradorPresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public AdminstradorPresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }





    }
}
