﻿
using RO.RentOfit.Domain.DTOs.Cliente;

namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructure
    {
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
    }
}