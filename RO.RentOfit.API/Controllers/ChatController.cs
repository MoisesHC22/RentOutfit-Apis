using Microsoft.AspNetCore.Mvc;
using RO.RentOfit.Domain.DTOs.Chat;
using RO.RentOfit.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RO.RentOfit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarConversacion([FromBody] IniciarChatDto iniciarChatDto)
        {
            var chatId = await _chatService.CrearConversacionAsync(iniciarChatDto.userId, iniciarChatDto.establecimientoId);
            return Ok(new { ChatId = chatId });
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarMensaje([FromBody] EnviarMensajeDto enviarMensajeDto)
        {
            await _chatService.EnviarMensajeAsync(enviarMensajeDto.ChatId, enviarMensajeDto.RemitenteId, enviarMensajeDto.Contenido);
            return Ok();
        }

        [HttpPost("{chatId}/mensajes")]
        public async Task<IActionResult> ObtenerMensajes([FromRoute] string chatId)
        {
            var mensajes = await _chatService.ObtenerMensajesAsync(chatId);
            return Ok(mensajes);
        }
    }
}
