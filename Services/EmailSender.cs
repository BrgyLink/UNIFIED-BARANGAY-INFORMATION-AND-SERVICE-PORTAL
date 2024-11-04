using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;


namespace BrgyLink.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // For testing, you can log or simulate sending an email.
            Console.WriteLine($"Sending email to {email} with subject: {subject}");
            return Task.CompletedTask;
        }
    }
}
