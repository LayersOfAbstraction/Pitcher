using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pitcher.Models;

namespace Pitcher.Models
{
    public class AuthenticationServiceUserProfile
    {

        public string UserEmailAddress { get; set; }

        public string UserName { get; set; }        
        
        public string UserProfileImage { get; set; }
    }
}