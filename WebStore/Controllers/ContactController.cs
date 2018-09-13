using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string sender, string password, string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(sender, sender);
                    var receiverEmail = new MailAddress(receiver, receiver);
                    var senderPassword = password;
                    var emailSubject = subject;
                    var emailMessage = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, senderPassword)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = emailSubject,
                        Body = emailMessage
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong. The message has not been sent.";
            }
            return View();
        }
    }
}
