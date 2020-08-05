using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using FPJobBoard.UI.EF.Models;

namespace FPJobBoard.UI.EF.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact(string divId)
        {
            ViewBag.Scroll = divId;

            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }


            string message = $"You have recived an email form {cvm.Name} with a subject <strong>{cvm.Subject}" +
                $"</strong>. Please respond to <em>{cvm.Email}</em> with your response to the following message:<br>" +
                $"<br> {cvm.Message}";

            MailMessage msg = new MailMessage("admin@tylerboyles.com", "tylermboyles@outlook.com",
                $"{System.DateTime.Now.Date} - {cvm.Subject}", message);

            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.tylerboyles.com");

            client.Credentials = new NetworkCredential("admin@tylerboyles.com", "Flashlight99!");
            //For AT&T or XFinity
            client.Port = 8889;

            try
            {
                client.Send(msg);
            }
            catch (System.Exception)
            {
                ViewBag.Error = "Sorry, there was an error handling your request. Please try again.";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);

        }
    }
}