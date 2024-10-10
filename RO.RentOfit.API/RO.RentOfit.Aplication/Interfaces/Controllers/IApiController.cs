using RO.RentOfit.Domain.Interfaces.Services;

namespace RO.RentOfit.Aplication.Interfaces.Controllers;
public interface IApiController
{
    IClientePresenter ClientePresenter { get; }
}
