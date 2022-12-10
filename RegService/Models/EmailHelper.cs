using RegService.ViewModel;
using System.Net.Mail;

namespace RegService.Models
{
    public class EmailHelper
    {
        public bool SendEmailTwoFactorCode(string userEmail, string code)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("developerpiyush1102k2@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Two Factor Code";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = code;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("developerpiyush1102k2@gmail.com", "emxjfvkspsrfubms");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("developerpiyush1102k2@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("developerpiyush1102k2@gmail.com", "emxjfvkspsrfubms");
            client.Host = "smtp.gmail.com";
			client.EnableSsl = true;
			client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public bool SendQueryEmail(RaiseQueryViewModel model)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("developerpiyush1102k2@gmail.com");
            mailMessage.To.Add(new MailAddress(model.SenderEmail));

            mailMessage.Subject = "Query raised on "+$"{model.Subject}";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = model.Message;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("developerpiyush1102k2@gmail.com", "emxjfvkspsrfubms");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }


        public bool SendCodeByLogin(UsersRegModel model, string code)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("developerpiyush1102k2@gmail.com");
            mailMessage.To.Add(new MailAddress(model.EmailId));

            mailMessage.Subject = "Your Otp Code.";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = code;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("developerpiyush1102k2@gmail.com", "emxjfvkspsrfubms");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }


        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("developerpiyush1102k2@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("developerpiyush1102k2@gmail.com", "emxjfvkspsrfubms");
            client.Host = "smtp.gmail.com";
			client.EnableSsl = true;
			client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
