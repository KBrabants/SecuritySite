﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.IO;
using System.Security.Policy;
using System.Web;

namespace SecuritySite.Services
{
    public class EmailingService
    {
        private string m_HostName;
        IHostEnvironment _environment { get; }
        public EmailingService(IHostEnvironment environment) {
            m_HostName = "support@voltic.tech";
            _environment = environment;
        }

        public async Task EmailVoltic(string subjectEmail, string Body)
        {
           await SendEmailAsync("support@voltic.tech", subjectEmail, Body);

        }
        public async Task EmailVerification(string emailTo, string verificationLink)
        {
            verificationLink = HttpUtility.UrlEncode(verificationLink);
            string subject = "Voltic Email Verification";
            string body = "<h1>Voltic Email Verification</h1> <br/>" +
                "<p>click the link below to verify your account.</p> <br/>" +
                $"<a href=\"www.voltic.tech/account/login?token={verificationLink}&handler=Verification&email={emailTo}\">Voltic Verification <br/> www.voltic.tech/account/login?token={verificationLink}</a>" ;

            await SendEmailAsync(emailTo, subject, body);

        }
        
        public async Task EmailCertificateRequest(string emailTo)
        {
            string subject = "Voltic Certificate Request Submittion Confirmation";
            string body = "<p>Your Certificate of alarm has been properly submitted and is under review.</p>";

            await SendEmailAsync(emailTo, subject, body);
        }

        public async Task AccountInfoUpdated(string emailTo)
        {
            string subject = "Voltic Certificate Request Submittion Confirmation";
            string body = "<h1>Account info has been updated</h1> <br/>" +
                "<p>Your account info has been updated. If this wasn't you, please contact us directly at 561-814-0042</p>";

            await SendEmailAsync(emailTo, subject, body);
        }
        public async Task AccountDeleted(string emailTo)
        {
            string subject = "Voltic Certificate Request Submittion Confirmation";
            string body = "<p>Your account info has been updated. If this wasn't you, please contact us directly at 561-814-0042</p>";

            await EmailVoltic("Account Deleted", "");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Task.Run(() =>
            {
                try
                {
                    var pickUpPathDirectory = Path.Combine(_environment.ContentRootPath, "Email");
                    var m_message = new MimeMessage();
                    m_message.To.Add(MailboxAddress.Parse(email));
                    m_message.From.Add(new MailboxAddress("Voltic Auto Email", "support@voltic.tech"));
                    m_message.Subject = subject;
                    m_message.Body = new TextPart("html")
                    {
                        Text = htmlMessage
                    };

                    // SaveToPickupDirectory(m_message, pickUpPathDirectory).Wait();

                    SmtpClient smtpClient = new SmtpClient();
                    try
                    {
                        smtpClient.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                        smtpClient.Authenticate("support@voltic.tech", "_GentileDolphins36912");
                        smtpClient.Send(m_message);

                    }
                    catch (Exception ex)
                    {
                        //  log errors
                    }
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString()); Error logging
                    Task.FromException(ex);
                }
            });
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
