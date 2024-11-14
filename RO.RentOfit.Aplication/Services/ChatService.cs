using RO.RentOfit.Domain.DTOs.Chat;
using RO.RentOfit.Domain.Interfaces.Services;

namespace RO.RentOfit.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatService _chatRepository;

        public ChatService(IChatService chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<string> CrearConversacionAsync(string userId, string establecimientoId)
        {
            return await _chatRepository.CrearConversacionAsync(userId, establecimientoId);
        }

        public async Task EnviarMensajeAsync(string chatId, string remitenteId, string contenido)
        {
            await _chatRepository.EnviarMensajeAsync(chatId, remitenteId, contenido);
        }

        public async Task<List<MensajeDto>> ObtenerMensajesAsync(string chatId)
        {
            return await _chatRepository.ObtenerMensajesAsync(chatId);
        }
    }
}
