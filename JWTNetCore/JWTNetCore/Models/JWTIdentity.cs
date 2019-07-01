using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTNetCore.Models
{
    public class JWTIdentity
    {
        public const string Issuer = "JWTExample";
        public const string Audience = "Par.com";
        public const string Key = "3235656898989823";
        public const string Schema = "Identity.Application"+","+JwtBearerDefaults.AuthenticationScheme;

    }
}
