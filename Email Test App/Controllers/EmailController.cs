using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Email_Test_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Email_Test_App.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(Email model)
        {
            using (MailMessage mm = new MailMessage(model.Login, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                if(model.CC != null)
                {
                   mm.CC.Add(model.CC);
                }
                if (model.BCC != null)
                {
                    mm.Bcc.Add(model.BCC);
                }
                mm.IsBodyHtml = false;
                if (model.Attachment !=null)
                {
                    foreach (var file in model.Attachment)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        mm.Attachments.Add(new Attachment(file.OpenReadStream(), fileName));
                    }
                    
                }
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = model.ServerAddress;
                    smtp.EnableSsl = model.IsSSL;
                    NetworkCredential NetworkCred = new NetworkCredential(model.Login, model.Password) ;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = model.Port;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent.";
                }
            }

            return View();
        }
    }
}