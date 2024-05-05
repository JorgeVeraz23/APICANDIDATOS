
using System.Net.Mail;
using System.Net;
using System.Text.Json.Serialization;
using Data.Interfaces.TemplateInterfaces;

namespace LinkQuality.Api.Services
{
    public class EmailService
    {
        private readonly IEmailTemplate _plantillaCorreo;

        public class MailRepository
        {
            private readonly SmtpClient _mailClient;
            private readonly string _username;

            public MailRepository(IConfiguration configuration)
            {
                string password;
                string host;
                int port;
                bool ssl;

                _username = "notificaciones@apptelink.net";
                password = "Tye123Nm";
                host = "outlook.office365.com";
                port = 587;
                ssl = true;
                
                //Lo dejo asi para futura modificaciones con produccion y desarrollo
                // Ejemplo...
                /*
                if (configuration["Ambiente"] == "Producción")
                {/*
                    _username = "postventadt@conauto.com.ec";
                    password = "pVt4DTC*n4ut0!2023";
                    host = "outlook.office365.com";
                    port = 587;
                    ssl = true;
                    _username = "postventadt@conauto.com.ec";
                    password = "pVt4DTC*n4ut0!2023";
                    host = "outlook.office365.com";
                    port = 587;
                    ssl = true;
                }
                else
                {
                    _username = "notificaciones@apptelink.net";
                    password = "Tye123Nm";
                    host = "smtp.office365.com";
                    port = 587;
                    ssl = true;
                }*/

                _mailClient = new SmtpClient
                {
                    Credentials = new NetworkCredential(_username, password),
                    EnableSsl = ssl,
                    Host = host,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = port
                };
            }

            public async Task<MailRepositoryResponse> SendEmail(string correo, string subject, string body, Dictionary<string, byte[]> attachments = null)
            {
                try
                {
                    var emailMessage = new MailMessage
                    {
                        From = new MailAddress(_username, "BanaCheck"),
                        Subject = subject,
                        
                        Body = body,
                        IsBodyHtml = true
                    };

                    if (attachments != null)
                    {
                        foreach (var attachment in attachments)
                        {
                            var attachmentObject = new Attachment(new MemoryStream(attachment.Value), attachment.Key);
                            emailMessage.Attachments.Add(attachmentObject);
                        }
                    }
                    // Para muchos correos
                    /*
                    foreach (var toEmail in toEmails)
                    {
                        emailMessage.To.Add(toEmail);
                    }*/
                    emailMessage.To.Add(correo);
                    await _mailClient.SendMailAsync(emailMessage);

                    return new MailRepositoryResponse { Successful = true, Message = "Correo enviado correctamente" };
                }
                catch (Exception e)
                {
                    return new MailRepositoryResponse { Successful = false, Message = e.Message };
                }
            }
        }
    }

    public class MailRepositoryResponse
    {
        [JsonPropertyName("succesful")]
        public bool Successful { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }

    public class EmailMessageOnly
    {/*
        public static string Template(string usuario, string contrasenia)
        {
            return @$"
                Saludos cordiales,<br>
                Estimado {usuario} su contraseña ha sido reestablecido<br>
                Nueva contraseña: {contrasenia}<br>
                Gracias por su atención.";

        }*/

        public static string TemplateRegisterInspectionNotification(string userName, string typeInspection, string nameInspector) => @$"
                ¡Hola!, <br>{userName}<br>
                Se notifica un registro de inspecction preliminar de tipo <b>{typeInspection}</b><br>
                realizada por el inspector {nameInspector}";


        public static string TemplateForgotPassword(string userName, string newPassword) => @$"
                ¡Hola!, <br><br>
                Parece que has olvidado tu contraseña de Banacheck.<br>
                No te preocupes, te generamos esta contraseña de momento: <b> {newPassword} </b> <br><br>
                Así puedes iniciar sesión y cambiar a una contraseña más cómoda para ti.<br>
                Te esperamos de vuelta.";

        public static string Template(string[] parametros, string body)
        {
            body = string.Format(body, parametros);
            return body;
        }
    

    //private async Task<MessageInfoDTO> EnviarEmail(string email, string enumerador, string[] parametros)
    //    {

    //        try
    //        {
    //            ItemEmailTempalteDTO plantillaCorreo = await _plantillaCorreo.BuscarPlantillaCorreo(enumerador);

    //            var body = EmailMessageOnly.Template(parametros, plantillaCorreo.Body);

    //            var result = await _emailManager.SendEmail(email, plantillaCorreo.Subject, body, null);

    //            if (result.Successful)
    //            {
    //                return infoDTO.AccionCompletada("Recuperacion de contraseña exitosa, revise su correo electronico");
    //            }
    //            return infoDTO.AccionFallida("El envio del correo ha fallado, intente nuevamente", 400);
    //        }
    //        catch (System.FormatException)
    //        {
    //            return infoDTO.AccionFallida("El numero de parametros no coincide con la plantilla, por favor agregue los parametros faltantes", 400);
    //        }
    //        catch (Exception ex)
    //        {
    //            return infoDTO.ErrorInterno(ex, "ApplicationUserManager", "Error al enviar el correo de recuperacion de contraseña.");
    //        }

    //    }
    }
}
