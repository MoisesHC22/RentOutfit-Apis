
using RO.RentOfit.Aplication.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.RentOfit.Aplication.Controllers
{
    public class ApiController:IApiController
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

        public IPersonaPresenter PersonaPresenter => new PersonaPresenter(_unitRepository, _mapper);    //
    }
}
