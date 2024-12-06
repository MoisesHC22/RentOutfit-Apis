
namespace RO.RentOfit.Infraestructure.Repositories
{
    public class StorageFirebaseConfig
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "rentoutfit-712b4.appspot.com";
        private readonly IConfiguration _configuration;
        private readonly FirebaseClient _firebaseClient;

        public StorageFirebaseConfig(IConfiguration configuration)
        {
            _configuration = configuration;

            var firebaseJson = @$"{{ 
                ""type"": ""{_configuration["Firebase:Type"]}"",
                ""project_id"": ""{_configuration["Firebase:ProjectId"]}"",
                ""private_key_id"": ""{_configuration["Firebase:PrivateKeyId"]}"",
                ""private_key"": ""{_configuration["Firebase:PrivateKey"]}"",
                ""client_email"": ""{_configuration["Firebase:ClientEmail"]}"",
                ""client_id"": ""{_configuration["Firebase:ClientId"]}"",
                ""auth_uri"": ""{_configuration["Firebase:AuthUri"]}"",
                ""token_uri"": ""{_configuration["Firebase:TokenUri"]}"",
                ""auth_provider_x509_cert_url"": ""{_configuration["Firebase:AuthProviderX509CertUrl"]}"",
                ""client_x509_cert_url"": ""{_configuration["Firebase:ClientX509CertUrl"]}"",
                ""universe_domain"": ""{_configuration["Firebase:UniverseDomain"]}"" 
            }}";

            var googleCredential = GoogleCredential.FromJson(firebaseJson);

            // Inicializando el StorageClient con las credenciales de Google
            _storageClient = StorageClient.Create(googleCredential);

            // Obtener el token de acceso para la conexión a Firebase
            string accessToken = googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync("https://www.googleapis.com/auth/firebase.database").Result;

            _firebaseClient = new FirebaseClient(
               "https://rentoutfit-712b4-default-rtdb.firebaseio.com/",
               new FirebaseOptions
               {
                   AuthTokenAsyncFactory = () => Task.FromResult(accessToken)
               });
        }

        // Método para acceder al FirebaseClient
        public FirebaseClient GetFirebaseClient()
        {
            return _firebaseClient;
        }

        // Método para subir archivos a Firebase Storage
        public async Task<string> SubirArchivo(IFormFile archivo, string nombre, string ubicacion)
        {
            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo de imagen no es válido.");
            }

            var fileName = $"{ubicacion}{nombre}{Path.GetExtension(archivo.FileName)}";

            using (var stream = archivo.OpenReadStream())
            {
                await _storageClient.UploadObjectAsync(_bucketName, fileName, archivo.ContentType, stream);
            }

            var publicUrl = $"https://storage.googleapis.com/{_bucketName}/{fileName}";
            return publicUrl;
        }

        // Método para manejar el carrito de compras
        public async Task CarritoCompras(CarritoAggregate requerimientos)
        {
            try
            {
                var usuarioCarrito = _firebaseClient
                    .Child("carritoCompras")
                    .Child(requerimientos.usuarioID.ToString());

                if (requerimientos.itemsCarrito == null || !requerimientos.itemsCarrito.Any())
                {
                    await usuarioCarrito.DeleteAsync();
                    Console.WriteLine("Carrito eliminado correctamente.");
                    return;
                }

                var carritoExistente = await usuarioCarrito.OnceSingleAsync<List<ItemsCarrito>>();
                List<ItemsCarrito> listaActualizada = carritoExistente ?? new List<ItemsCarrito>();

                foreach (var nuevoItem in requerimientos.itemsCarrito)
                {
                    var itemExistente = listaActualizada.FirstOrDefault(i => i.vestimentaID == nuevoItem.vestimentaID);
                    if (itemExistente != null)
                    {
                        if (nuevoItem.stock > 0)
                        {
                            itemExistente.stock = nuevoItem.stock;
                            itemExistente.fechaPrestamo = nuevoItem.fechaPrestamo;
                        }
                        else
                        {
                            listaActualizada.Remove(itemExistente);
                        }
                    }
                    else
                    {
                        if (nuevoItem.stock > 0)
                        {
                            listaActualizada.Add(nuevoItem);
                        }
                    }
                }

                await usuarioCarrito.PutAsync(listaActualizada);

                Console.WriteLine("Carrito actualizado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en Firebase: {ex.Message}");
                throw;
            }
        }

        // Método para obtener el carrito de compras de un usuario
        public async Task<List<ItemsCarrito>> ObtenerCarritoCompras(int usuarioID)
        {
            try
            {
                var usuarioCarrito = _firebaseClient
                    .Child("carritoCompras")
                    .Child(usuarioID.ToString());

                var carritoExistente = await usuarioCarrito.OnceSingleAsync<List<ItemsCarrito>>();

                return carritoExistente ?? new List<ItemsCarrito>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el carrito de Firebase: {ex.Message}");
                return new List<ItemsCarrito>();
            }
        }

        // Nuevo método para iniciar una conversación de chat
        public async Task<string> IniciarConversacion(string userId, string establecimientoId)
        {
            var chat = new
            {
                UserId = userId,
                EstablecimientoId = establecimientoId,
                Mensajes = new List<object>()
            };

            var chatRef = await _firebaseClient
                .Child("chats")
                .PostAsync(chat);

            return chatRef.Key; // Devuelve el ID del chat generado
        }

        // Nuevo método para enviar un mensaje en el chat
        public async Task EnviarMensaje(string chatId, string remitenteId, string contenido)
        {
            var mensaje = new
            {
                RemitenteId = remitenteId,
                Contenido = contenido,
                Timestamp = DateTime.UtcNow
            };

            await _firebaseClient
                .Child("chats")
                .Child(chatId)
                .Child("Mensajes")
                .PostAsync(mensaje);
        }

        // Nuevo método para obtener los mensajes de un chat
        public async Task<List<MensajeDto>> ObtenerMensajes(string chatId)
        {
            var mensajes = await _firebaseClient
                .Child("chats")
                .Child(chatId)
                .Child("Mensajes")
                .OnceAsync<MensajeDto>();

            return mensajes?.Select(m => m.Object).ToList() ?? new List<MensajeDto>();
        }
    }
}
