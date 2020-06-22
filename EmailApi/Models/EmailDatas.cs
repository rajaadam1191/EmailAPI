using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailApi.Models
{
    public class EmailDatas
    {
        public string UserName { get; set; }
        public string smtp { get; set; }
        public int port { get; set; }
        public bool EnableSsl { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string sender { get; set; }
        public string receiver { get; set; }
        public string ccInfo { get; set; }
        public string attachmentStatus { get; set; }
        public string attachment { get; set; }
        public string status { get; set; }
        public string dateat { get; set; }

        public string errorInfo { get; set; }
    }
}