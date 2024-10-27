
namespace RO.RentOfit.Infraestructure.Security
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);  // Usa BCrypt.Net.BCrypt aquí
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);  // Usa BCrypt.Net.BCrypt aquí
        }
    }
}
