namespace RouteCourse.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from, string reciptients, string subject, string body );
	}
}
