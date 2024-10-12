
namespace RO.RentOfit.Infraestructure.Repositories
{
    public class StorageFirebaseConfig
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "rentoutfit-712b4.appspot.com";

        public StorageFirebaseConfig() 
        {
            var googleCredential = GoogleCredential.FromFile(@"C:\Firebase\rentoutfit-712b4-firebase-adminsdk-8a650-1bfc5490c5.json");

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
