using DTOs.DTOs;
using DTOs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsereServices _usereServices;
        private readonly IConfiguration _configuration;
        public AuthController(IUsereServices usereServices, IConfiguration configuration)
        {
            _usereServices = usereServices;
            _configuration = configuration;
        }




        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(AuthRequestObject authObj, CancellationToken cancellationtoken)
        {
            var obj = await _usereServices.FindUsereByEmail(authObj.Email, cancellationtoken);
            if (obj != null && BCrypt.Net.BCrypt.Verify(authObj.Password, obj.UserPassword))
            {
                //var userRoles = await _userRepository.GetUserRoles(obj.UserId, cancellationtoken);
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, obj.Email),
                //    new Claim(ClaimTypes.NameIdentifier, obj.UserId.ToString())
                //};
                //userRoles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:Expiration"]));

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    //claims,
                    expires: expiry,
                    signingCredentials: signIn
                );

                return Ok(new TokenDTO { Token = new JwtSecurityTokenHandler().WriteToken(token) });

            }
            return BadRequest();
        }
    }
}
