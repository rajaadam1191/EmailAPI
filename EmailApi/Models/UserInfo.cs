using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailApi.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}