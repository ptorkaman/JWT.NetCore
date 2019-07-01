using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTNetCore.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {

        }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
