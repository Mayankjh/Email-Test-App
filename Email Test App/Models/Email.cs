using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Email_Test_App.Models
{
    public class Email
    {
        [Required]
        public string ServerAddress { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsSSL { get; set; }
        [Required]
        public int Port { get; set; }

        [Required]
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public List<IFormFile> Attachment { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
