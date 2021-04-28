using System.Threading.Tasks;

namespace TokenServiceApi.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
