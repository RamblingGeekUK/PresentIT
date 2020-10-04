using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PresentIT.Models
{
    public class UserProfileViewModel
    {

        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string UserID { get; set; }
        public bool Role { get; set; }
    }
}
