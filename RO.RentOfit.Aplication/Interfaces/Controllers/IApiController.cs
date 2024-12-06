﻿
namespace RO.RentOfit.Aplication.Interfaces.Controllers;
public interface IApiController
{
    IClientePresenter ClientePresenter { get; }
    IListasPresenter ListasPresenter { get; }
    IVendedorPresenter vendedorPresenter { get; }
    IRecuperarContrasenaPresenter recuperarContrasenaPresenter { get; }
    IAdministradorPresenter administradorPresenter { get; }
}
