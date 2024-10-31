using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RO.RentOfit.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        // Constructor que inyecta la configuración para acceder a los valores de EmailSettings
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarMsj(string email, string titulo, string mensaje) 
        {
            try 
            {

                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderPassword = _configuration["EmailSettings:SenderPassword"];


                var correo = new MailMessage
                {
                    From = new MailAddress(senderEmail),  // Añadimos la dirección del remitente
                    Subject = titulo,
                    Body = mensaje,
                    IsBodyHtml = true  // Si el correo debe tener formato HTML
                };
                correo.To.Add(email);  // Añadir el destinatario

                // Configurar el cliente SMTP
                using (var smtp = new SmtpClient(smtpServer))
                {
                    smtp.Port = smtpPort;  // Puerto configurado para el servicio de correo (por ejemplo, 587 para Gmail con SSL)
                    smtp.Credentials = new NetworkCredential(senderEmail, senderPassword); // Credenciales de correo
                    smtp.EnableSsl = true;  // Asegurar que SSL está habilitado para una conexión segura

                    // Enviar el correo
                    await smtp.SendMailAsync(correo);
                }

                Console.WriteLine($"El Correo de { titulo } fue enviado con éxito.");

            }
            catch (SmtpFailedRecipientException ex)
            {
                // Manejar el error cuando el correo no puede ser enviado a un destinatario específico
                Console.WriteLine($"Error al enviar el correo a {ex.FailedRecipient}: {ex.Message}");
            }
            catch (SmtpException ex)
            {
                // Manejar errores específicos de SMTP (servidor de correo)
                Console.WriteLine($"Error SMTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de error
                Console.WriteLine($"Error general al enviar el correo: {ex.Message}");
            }

        }

    }
}
