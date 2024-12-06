namespace RO.RentOfit.Domain.DTOs.Chat
{
    public class ChatDto
    {
        public string ChatId { get; set; }
        public string userId { get; set; }
        public string establecimientoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public List<MensajeDto> Mensajes { get; set; }
    }
}
