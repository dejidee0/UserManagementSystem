using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserProfileData.Domain;
using UserProfileServices.UserServices;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
        if (string.IsNullOrEmpty(_emailSettings.SenderEmail))
        {
            throw new ArgumentException("Sender email address is required and cannot be empty.", nameof(_emailSettings.SenderEmail));
        }
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        // Validate the toEmail parameter
        if (string.IsNullOrEmpty(toEmail))
        {
            throw new ArgumentException("To email address is required and cannot be empty.", nameof(toEmail));
        }

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));

        try
        {
            // Attempt to parse and add the recipient email address
            email.To.Add(MailboxAddress.Parse(toEmail));
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid to email address format.", nameof(toEmail), ex);
        }

        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using (var smtp = new SmtpClient())
        {
            try
            {
                await smtp.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new InvalidOperationException("Failed to send email", ex);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}