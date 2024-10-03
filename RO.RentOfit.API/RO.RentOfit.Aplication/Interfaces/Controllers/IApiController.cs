using RO.RentOfit.Domain.Interfaces.Services;

namespace RO.RentOfit.Aplication.Interfaces.Controllers;
public interface IApiController
{
    IPersonaPresenter PersonaPresenter { get; }
}
