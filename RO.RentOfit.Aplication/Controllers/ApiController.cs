
namespace RO.RentOfit.Aplication.Controllers
{
    public class ApiController : IApiController
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public ApiController(IUnitRepository unitRepository, IMapper mapper, IConfiguration configuration)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IClientePresenter ClientePresenter => new ClientePresenter(_unitRepository, _mapper);
        public IListasPresenter ListasPresenter => new ListasPresenter(_unitRepository, _mapper);
        public IVendedorPresenter vendedorPresenter => new VendedorPresenter(_unitRepository, _mapper);
        public IRecuperarContrasenaPresenter recuperarContrasenaPresenter => new RecuperarContrasenaPresenter(_unitRepository, _mapper);
        public IAdministradorPresenter administradorPresenter => new AdminstradorPresenter(_unitRepository, _mapper);


    }
}
