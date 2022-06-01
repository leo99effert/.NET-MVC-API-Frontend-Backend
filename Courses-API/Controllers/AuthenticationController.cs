using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Courses_API.ViewModels.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class AuthenticationController : ControllerBase
    {
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AuthenticationController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _config = config;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserViewModel>> RegisterUser(RegisterUserViewModel model)
    {
        var user = new IdentityUser
        {
            Email = model.Email!.ToLower(),
            UserName = model.Email.ToLower()
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if(result.Succeeded)
        {
            if(model.IsAdmin)
            {
                await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
            }

            await _userManager.AddClaimAsync(user, new Claim("User", "true"));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));

            var userData = new UserViewModel
            {
                UserName = user.UserName,
                Token = await CreateJwtToken(user)
            };

            return StatusCode(201, userData); // StatusCode(201) == Created
        }
        else 
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("User registrations", error.Description);
            }
            return StatusCode(500, ModelState); // StatusCode(500) == Internal Server Error
        }
    }

    [HttpPost("login")]
        public async Task<ActionResult<UserViewModel>> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user is null)
                return StatusCode(401, "Invalid username"); // StatusCode(401) == Unauthorized    

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(!result.Succeeded)
                return StatusCode(401); // StatusCode(401) == Unathorized

            var userData = new UserViewModel
            {
                UserName = user.UserName,
                Token = await CreateJwtToken(user)
            };

            return StatusCode(200, userData); // StatusCode(200) == Ok
        }

        private async Task<string> CreateJwtToken(IdentityUser user)
        {
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey"));

            var userClaims = await _userManager.GetClaimsAsync(user);

            var jwt = new JwtSecurityToken
            (
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        

        //   ALternative CreateJwtToken method
        //
        // private string CreateJwtToken(string userName)
        // {
        //     var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey"));

        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, userName),
        //         new Claim("Admin", "true")
        //     };

        //     var jwt = new JwtSecurityToken
        //     (
        //         claims: claims,
        //         notBefore: DateTime.Now,
        //         expires: DateTime.Now.AddDays(7),
        //         signingCredentials: new SigningCredentials
        //         (
        //             new SymmetricSecurityKey(key),
        //             SecurityAlgorithms.HmacSha512Signature
        //         )
        //     );
        //     return new JwtSecurityTokenHandler().WriteToken(jwt);
        // }
    }
}