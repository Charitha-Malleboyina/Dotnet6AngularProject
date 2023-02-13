using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{

    [EnableCors("SiteCorsPolicy")]
  
    public class RegisterUserController : Controller
    {
        private readonly IRegisterUser _registerUser;
        private readonly IConfiguration _config;
        public RegisterUserController(IRegisterUser registerUser,IConfiguration config)
        {
            _registerUser = registerUser;
            _config = config;
        }

        [HttpGet]
      
        [Route("GetUsers")]
        public async Task<IActionResult> GetallUsers()
        {
            var user = await _registerUser.GetAllUserDetails();
            return Ok(user);
        }


        [HttpPost]
        [Route("PostUsers")]
        public async Task<IActionResult> AddUsers([FromBody] RegisterUsersModel registerUsersModel)
        {
            var user = await _registerUser.AddUsersDetails(registerUsersModel);
            return Ok(user);
        }
        [HttpPut]
        [Route("UpdateUsers/{id}")]
        public async Task<IActionResult> EditUsers(Guid id, [FromBody] RegisterUsersModel registerUsersModel)
        {
            var user = await _registerUser.EditUsersDetails(id,registerUsersModel);
            return Ok(user);
        }

        [HttpDelete]
        [Route("DeleteUsers/{id}")]
        public async Task<IActionResult> DeleteUsers(Guid id)
        {
            var user = await _registerUser.DeleteUsersDetails(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("loginUsers")]
        public async Task<IActionResult> loginUsers([FromBody]LoginModel login)
        {

            var user = await _registerUser.GetDetails(login);
            if (user != false )
            {
                var token = GenerateToken(login.email);

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Logged in Successfully",
                    JwtToken = token
                });
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet]
        [Route("getToken")]
        public async Task<IActionResult> GetToken(string username)
        {
            try
            {
                var tokenval = GenerateToken(username);
                return Ok(tokenval);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private string GenerateToken(string username)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(ClaimTypes.Email,username),
            };
            var Token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : credential);
            return tokenhandler.WriteToken(Token);
        }

        [HttpGet]
        [Route("getroles")]
        public async Task<IActionResult> GetRoles()
        {
            var data = await _registerUser.GetRoleTypes();
            return Ok(data);
        }


        [HttpGet]
        [Route("GetUserDetailsbyEmail")]
        public async Task<IActionResult> GetUserDetailsbyEmail(string email)
        {
            var data = await _registerUser.GetDetailsbyid(email);
            return Ok(data);
        }

        [HttpGet]
        [Route("getuserbyemail")]
        public async Task<IActionResult> GetUsersbyFirstname(string FirstName)
        {
            var data = await _registerUser.GetDetailsbyName(FirstName);
            return Ok(data);
        }
    }
}
