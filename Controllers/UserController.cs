using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ComicBookAPI.Data;
using ComicBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ComicBookAPI.Controllers {
    [Route ("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly ApiContext _context;
        private readonly JwtSetting jwtSetting;

        public UserController (ApiContext context, IOptions<JwtSetting> options) {
            _context = context;
            jwtSetting = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate (User user) {
            
            if(user.Username != "admin" && user.Password != "admin")
                return Unauthorized ();
            
            // Generating token
            var tokenHandler = new JwtSecurityTokenHandler ();
            var tokenKey = Encoding.UTF8.GetBytes (jwtSetting.securitykey);
            var tokenDesc = new SecurityTokenDescriptor {
                Expires = DateTime.Now.AddSeconds (100),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (tokenKey), SecurityAlgorithms.Sha256)
            };
            string finalToken = tokenHandler.WriteToken (tokenHandler.CreateToken (tokenDesc));

            return Ok (finalToken);
        }

    }

}