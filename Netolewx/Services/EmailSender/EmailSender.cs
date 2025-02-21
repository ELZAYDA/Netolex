using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace RouteCourse.Services.EmailSender
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
        {
			_configuration=configuration;
		}
        public async Task SendAsync(string from, string reciptients, string subject, string body)
		{
			//[from]account I will Send from 
			var SenderEmail = _configuration["EmailSettings:SenderEmail"];
			var SenderPassword = _configuration["EmailSettings:SenderPassword"];

			var EmailMassage = new MailMessage();
			EmailMassage.From=new MailAddress(from);
			EmailMassage.To.Add(reciptients);
			EmailMassage.Subject=subject;
			EmailMassage.Body=$"<html><body>{body}</body></html>";
			EmailMassage.IsBodyHtml=true;

			var smtpClient = new SmtpClient(
		_configuration["EmailSettings:SmtpClientServer"],
		int.Parse(_configuration["EmailSettings:SmtpClientPort"]))
			{
				Credentials = new NetworkCredential(SenderEmail, SenderPassword),
				EnableSsl = true // all of it just for ENCRYPTION
			};

			// 🚀 Send the email
			await smtpClient.SendMailAsync(EmailMassage);
		}
	}
}
