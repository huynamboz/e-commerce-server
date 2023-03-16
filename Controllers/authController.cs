using e_commerce_server.Data;
using e_commerce_server.Modes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private MyDbContext _context;
        private readonly AppSettings _appSettings;

        public authController(MyDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpPost("login")]
        public IActionResult Validate(LoginModel model)
        {
            string pass = mahoa(model.password);
            var user = _context.Users.SingleOrDefault(p => p.email ==
            model.email && pass == p.password);
            if(user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    message = "Invalid username/password"
                });
            }

            //cap token
            return Ok(new
            {
                success = true,
                message = "login dc roi con mẹ nó",
                token = GenerateToken(user)
            });
        }
        MD5 md = MD5.Create();
        private string mahoa(string pass)
        {
            byte[] inp = Encoding.ASCII.GetBytes(pass);
            byte[] hash = md.ComputeHash(inp);

            StringBuilder s = new StringBuilder();
            for(int i =0; i < hash.Length; i++)
            {
                s.Append(hash[i].ToString("x2"));
            }
            return s.ToString();
        }
        private string GenerateToken(user user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secrectkeybytes = Encoding.UTF8.GetBytes(_appSettings.SecrectKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.name),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("id", user.user_id.ToString()),


                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secrectkeybytes), SecurityAlgorithms.HmacSha256Signature)
                
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
        [HttpPost("register")]
        public IActionResult registerNew(registerModel info)
        {
            var newUser = new user
            {
                phone_number = info.phone_number,
                email = info.email,
                password = mahoa(info.password),
                active_status = "not_active",
                name = info.name,
                created_at = DateTime.Now.ToString("yyyyMMdd"),
                roleID = 1,
                avatar_url = info.avatar_url != "" ? info.avatar_url : ""
            };
            _context.Add(newUser);
            _context.SaveChanges();
            return Ok(new
            {
                success = true,
            });
        }
        [HttpGet("user/{id}")]
        public IActionResult getInfoUser(string id)
        {
            var user = _context.Users.SingleOrDefault(userItem =>
            userItem.user_id.ToString() == id);
            if (user != null)
                return Ok(user);
            else return NotFound();
        }
    }
}
