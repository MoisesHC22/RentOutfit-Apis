
namespace RO.RentOfit.Infraestructure.Repositories
{
    public class StorageFirebaseConfig
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "rentoutfit-712b4.appspot.com";
        private readonly IConfiguration _configuration;

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
              
            _storageClient = StorageClient.Create(googleCredential);
        }

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
    }
}
