using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIDemo_Godrej.Models;

namespace WebAPIDemo_Godrej.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly WebAPIDBGodrejContext _context;

        public TokenController(IConfiguration configuration, WebAPIDBGodrejContext context)
        {
                _configuration = configuration;
            _context = context;
        }

        private async Task<UserInfo> GetUser(string username, string password)
        {
            //validate username and password against database table
            //if valid return UserInfo object
            var user = await _context.UserInfos.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user;

        }

        [HttpPost]
        public async Task<IActionResult> Post(UserInfo userInfo)
        {
            if (userInfo != null && userInfo.Username != null && userInfo.Password != null)
            {
                UserInfo user = await GetUser(userInfo.Username, userInfo.Password);

                if (user != null)
                {
                    //create claim role and store user role value received from database table
                    var claims = new Claim[] { new Claim(ClaimTypes.Role, user.Role) };

                    //issue token

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secretkey"]));

                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    //var token = new JwtSecurityToken(
                    //            issuer: _configuration["Issuer"],
                    //            audience: _configuration["Audience"],
                    //            expires: DateTime.Now.AddHours(1),
                    //            signingCredentials: credentials
                    //            );


                    var token = new JwtSecurityToken(
                               issuer: _configuration["Issuer"],
                               audience: _configuration["Audience"],
                               expires: DateTime.Now.AddHours(1),
                               signingCredentials: credentials,
                               claims: claims
                               );
                    //send token to client
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Please provide username and password");
            }
        }

    }
}
