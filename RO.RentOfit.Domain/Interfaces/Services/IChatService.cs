﻿using RO.RentOfit.Domain.DTOs.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IChatService
    {
        Task<string> CrearConversacionAsync(string userId, string establecimientoId);
        Task EnviarMensajeAsync(string chatId, string remitenteId, string contenido);
        Task<List<MensajeDto>> ObtenerMensajesAsync(string chatId);
    }
}