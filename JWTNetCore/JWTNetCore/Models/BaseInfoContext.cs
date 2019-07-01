using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTNetCore.Models
{
    public class JWTNetCoreContext : IdentityDbContext<AppUser>
    {
        public JWTNetCoreContext(DbContextOptions options) : base(options)
        {

        }
    }
}
