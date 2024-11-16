
namespace RO.RentOfit.Infraestructure.Repositories
{
    public class ChatFirebaseRepository : IChatService
    {
        private readonly FirebaseClient _firebaseClient;

        public ChatFirebaseRepository(StorageFirebaseConfig firebaseConfig)
        {
            _firebaseClient = firebaseConfig.GetFirebaseClient();
        }

        public async Task<string> CrearConversacionAsync(string userId, string establecimientoId)
        {
            var chatId = Guid.NewGuid().ToString();

            var nuevaConversacion = new
            {
                UserId = userId,
                EstablecimientoId = establecimientoId,
                FechaInicio = DateTime.UtcNow
            };

            await _firebaseClient
                .Child("chats")
                .Child(chatId)
                .PutAsync(nuevaConversacion);

            return chatId;
        }

        public async Task EnviarMensajeAsync(string chatId, string remitenteId, string contenido)
        {
            var nuevoMensaje = new MensajeDto
            {
                RemitenteId = remitenteId,
                Contenido = contenido,
                FechaEnvio = DateTime.UtcNow
            };

            await _firebaseClient
                .Child("chats")
                .Child(chatId)
                .Child("mensajes")
                .PostAsync(nuevoMensaje);
        }

        public async Task<List<MensajeDto>> ObtenerMensajesAsync(string chatId)
        {
            var mensajes = await _firebaseClient
                .Child("chats")
                .Child(chatId)
                .Child("mensajes")
                .OnceAsync<MensajeDto>();

            return mensajes.Select(m => m.Object).ToList();
        }
    }
}
