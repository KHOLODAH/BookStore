using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace BookStore2.Services
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // ما بيعملش أي حاجة
            return Task.CompletedTask;
        }
    }
}
