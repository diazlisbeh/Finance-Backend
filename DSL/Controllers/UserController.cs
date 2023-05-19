using System.Security.Cryptography;
using Backend.DAL.Models;
using Backend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.DAL.DTOs;
using Backend.DAL.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;


namespace Backend.DSL.Controller
{ 
    [EnableCors("WebPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private IConfiguration _config;

        public UserController(IUserService service, IConfiguration config){
            _service = service;
            _config = config;
        }

        [HttpPost("register")]
        [EnableCors]
        public async Task<IActionResult> Register(UserRegisterDto userDto){
            
            var user = await _service.Register(userDto);
            if(user is null) return StatusCode(409); //duplicate record
            return StatusCode(201);
        }

        [HttpGet]
        // [EnableCors]
        public IActionResult GetAll (){
            return Ok(_service.GetUsers());
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int  id)
        {
            var user = await _service.GetUser(id);
            return  Ok(user);
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login (UserLoginDto userLogin){
            var userRepo = await _service.Login(userLogin.email,userLogin.password);
            if(userRepo is null)return Unauthorized();

            var Claims = new []{
                new Claim(ClaimTypes.NameIdentifier,userRepo.Id.ToString()),
                new Claim(ClaimTypes.Email,userRepo.Email)
            }; 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var Card = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var TokenDescription = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = Card,
                
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(TokenDescription);
            return Ok(new{
                token = tokenHandler.WriteToken(token),
                userRepo.Name,
                userRepo.Capital,
                userRepo.Id,
                userRepo.LastName,
                userRepo.Email,
                userRepo.Status,
                userRepo.Budgets,
                userRepo.Transactions

            });

            
            
            
        }

    }
}