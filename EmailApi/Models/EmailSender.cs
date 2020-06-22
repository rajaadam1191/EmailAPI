using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailApi.Models
{
    public class EmailSender
    {
        public string toAddress { get; set; }
        public string toName { get; set; }
        public string toId { get; set; }
        public string subject { get; set; }
        public string ideaID { get; set; }
        public string content { get; set; }
        public string sender { get; set; }
        public string resci { get; set; }
        public string type { get; set; }
        public string subTitle { get; set; }
    }
}