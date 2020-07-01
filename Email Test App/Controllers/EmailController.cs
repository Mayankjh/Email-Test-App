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
            if (ModelState.IsValid)
            {
                string exmsg = "";
                try
                {
                    using (MailMessage mm = new MailMessage(model.From, model.To))
                    {
                        mm.From = new MailAddress(model.From, model.FromName);
                        mm.Subject = model.Subject;
                        mm.Body = model.Body;
                        if (model.CC != null)
                        {
                            mm.CC.Add(model.CC);
                        }
                        if (model.BCC != null)
                        {
                            mm.Bcc.Add(model.BCC);
                        }
                        mm.IsBodyHtml = true;
                        if (model.Attachment != null)
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
                            NetworkCredential NetworkCred = new NetworkCredential(model.Login, model.Password);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = model.Port;
                            smtp.Send(mm);
                            ModelState.Clear();
                            ViewBag.Message = "Email has been sent successfully.";
                        }
                    }
                }


                catch (Exception ex)
                {
                    exmsg = ex.Message;
                    ViewBag.Error = exmsg;
                }
                
            }
            return View();
         }
    }
   }