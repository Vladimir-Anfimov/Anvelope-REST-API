using AnvelopeApi.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Data
{
    public class Mailer : IMailer
    {
        private readonly IWebHostEnvironment _env;

        public Mailer(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("####", "####"));
                message.To.Add(new MailboxAddress(subject,email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 465, true);
                    }
                    else
                    {
                        await client.ConnectAsync("smtp.gmail.com");
                    }

                    await client.AuthenticateAsync("#####", "#####");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
