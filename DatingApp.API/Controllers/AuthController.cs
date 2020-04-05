
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _repo.UserExists(userForRegisterDto.UserName))
                return BadRequest("User alread exist.");

            var userTocreate = new User
            {
                Username = userForRegisterDto.UserName
            };
            var createdUser = await _repo.Register(userTocreate, userForRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            
            var userFromrepo = await _repo.Login(userForLoginDto.UserName.ToLower(), 
            userForLoginDto.Password);

            if (userFromrepo == null)
                return Unauthorized();

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,userFromrepo.Id.ToString()),
                 new Claim(ClaimTypes.Name,userFromrepo.Username)
             };
            var key = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires=System.DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };

            var tokenhandler=new JwtSecurityTokenHandler();
            var token=tokenhandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token=tokenhandler.WriteToken(token)
            });
        }
    }
}