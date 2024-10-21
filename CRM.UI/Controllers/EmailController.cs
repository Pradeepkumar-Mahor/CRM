using CRM.UI.Service.Email;
using Microsoft.AspNetCore.Mvc;

namespace CRM.UI.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var to = "smpradeepmahor@gmail.com";
            var subject = "Test";
            var message = "Hello Word";
            await _emailSender.SendEmailAsync(to, subject, message);
            return View();
        }
    }
}