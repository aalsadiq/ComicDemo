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
        private readonly JwtSettings jwtSettings;

        public UserController (ApiContext context, IOptions<JwtSettings> options) {
            _context = context;
            jwtSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate (User user) {
            var userCred =
                this._context.Users.FirstOrDefault (usr => usr.Username == user.Username && usr.Password == user.Password);
            if (userCred == null)
                return Unauthorized ();
            
            // Generating token
            var tokenHandler = new JwtSecurityTokenHandler ();
            var tokenKey = Encoding.UTF8.GetBytes (this.jwtSettings.securitykey);
            var tokenDesc = new SecurityTokenDescriptor {
                Expires = DateTime.Now.AddSeconds (100),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (tokenKey), SecurityAlgorithms.Sha256)
            };
            string finalToken = tokenHandler.WriteToken (tokenHandler.CreateToken (tokenDesc));

            return Ok (finalToken);
        }

    }

}