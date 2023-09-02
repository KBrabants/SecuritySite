using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.IO;
namespace SecuritySite.Services
{
    public class EmailingService : IEmailSender
    {
        private string m_HostName;
        public string Email { get; set; }
        IHostEnvironment _environment { get; }
        public EmailingService(IHostEnvironment environment) {
            m_HostName = "support@voltic.tech";
            _environment = environment;
        }

        public Task EmailVoltic(string subjectEmail, string Body)
        {
            SendEmailAsync("support@voltic.tech", subjectEmail, Body).Start();

            return Task.CompletedTask;
        }
        public Task EmailVerification(string emailTo, string verificationLink)
        {
            string subject = "Voltic Email Verification";
            string body = "<h1>Voltic Email Verification</h1> <br/>" +
                "<p>click the link below to verify your account.</p> <br/>" +
                $"{verificationLink}" ;

            SendEmailAsync(emailTo, subject, body).Start();

            return Task.CompletedTask;
        }
        
        public Task EmailCertificateRequest(string emailTo)
        {
            string subject = "Voltic Certificate Request Submittion Confirmation";
            string body = "<h1>Certificate Of Alarm Request</h1> <br/>" +
                "<p>Your Certificate of alarm has been properly submitted and is under review.</p>";

            SendEmailAsync(emailTo, subject, body).Start();
            return Task.CompletedTask;
        }

        public Task AccountInfoUpdated(string emailTo)
        {
            string subject = "Voltic Certificate Request Submittion Confirmation";
            string body = "<h1>Account info has been updated</h1> <br/>" +
                "<p>Your account info has been updated. If this wasn't you, please contact us directly at 561-814-0042</p>";

            SendEmailAsync(emailTo, subject, body).Start();
            return Task.CompletedTask;
        }
        public Task AccountDeleted(string emailTo)
        {
            string subject = "Voltic Certificate Request Submittion Confirmation";
            string body = "<h1>Account info has been updated</h1> <br/>" +
                "<p>Your account info has been updated. If this wasn't you, please contact us directly at 561-814-0042</p>";

            EmailVoltic("Account Deleted", "").Start();
            return Task.CompletedTask;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var pickUpPathDirectory = Path.Combine(_environment.ContentRootPath, "Email");
                var m_message = new MimeMessage();
                m_message.To.Add(MailboxAddress.Parse(email));
                m_message.From.Add(MailboxAddress.Parse("support@voltic.tech"));
                m_message.Subject = subject;
                m_message.Body = new TextPart("html")
                {
                    Text = htmlMessage
                };

                await SaveToPickupDirectory(m_message, pickUpPathDirectory);

                   

            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex.ToString()); Error logging
                await Task.FromException(ex);
            }

        }
        public static async Task SaveToPickupDirectory(MimeMessage message, string pickupDirectory)
        {
            do
            {
                var path = Path.Combine(pickupDirectory, Guid.NewGuid().ToString() + ".eml");
                Stream stream;

                try
                {
                    stream = File.Open(path, FileMode.CreateNew);
                }
                catch (IOException)
                {
                    if (File.Exists(path))
                        continue;
                    throw;
                }

                try
                {
                    using (stream)
                    {
                        using var filtered = new FilteredStream(stream);
                        filtered.Add(new SmtpDataFilter());

                        var options = FormatOptions.Default.Clone();
                        options.NewLineFormat = NewLineFormat.Dos;

                        await message.WriteToAsync(options, filtered);
                        await filtered.FlushAsync();
                        return;
                    }
                }
                catch
                {
                    File.Delete(path);
                    throw;
                }
            } while (true);
        }
    }
}
