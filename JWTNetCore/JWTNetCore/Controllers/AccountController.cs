using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTNetCore.Models;
using JWTNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTNetCore.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        [HttpPost("Login")]
        public async Task< IActionResult> Login([FromBody]UserLoginView model)
        {
            //var user = new AppUser
            //{
            //    UserName=model.UserName,

            //};
            var checkUser = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.CheckPasswordAsync(checkUser, model.Password);
            if (result)
            {
                return Json("Login");
            }
            else
            {
                return Json("error");
            }
            
        }
        [HttpPost("Register")]
        public async Task< IActionResult> Register([FromBody]RegisterUserView model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                //PasswordHash = model.Password,
                Email = model.Email
            };
           var result= await userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                return Json("ok");
            }
            else
            {
                return Json(result.Errors);
            }
        }
        // GET: api/<controller>
       
        [HttpGet("Get")]
        [Authorize(AuthenticationSchemes=JWTIdentity.Schema)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPost("Token")]
   public async Task<ActionResult> Token([FromBody]UserLoginView model)
        {
            var appUser =await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.CheckPasswordAsync(appUser, model.Password);
            if (result)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTIdentity.Key));
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,appUser.Id),
                    new Claim(JwtRegisteredClaimNames.UniqueName,model.UserName
                    )

                };

                var tokenJWT = new JwtSecurityToken(
                    JWTIdentity.Issuer,
                    JWTIdentity.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddDays(30),
                    signingCredentials: creds
                    );
                var tokenResult = new
                {
                    token=new JwtSecurityTokenHandler().WriteToken(tokenJWT),
                    expiration=tokenJWT.ValidTo
                };
                return Json(tokenResult);

            }
            else
            {
                return Json(BadRequest());
            }

        }

       
  
    }
}
