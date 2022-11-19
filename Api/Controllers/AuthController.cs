using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
//using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Api.Model;
using MediatR;
using CleanArchitecture.Infrastructure.Identity;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager;
    private IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    // /api/auth/register
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            var User = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = "admin",
                LastName = "admin",
            };

            var result = await _userManager.CreateAsync(User, model.Password);


            if (result.Succeeded)
                return Ok(result); // Status Code: 200 

            return BadRequest(result);
        }

        return BadRequest("Some properties are not valid"); // Status code: 400
    }

    // /api/auth/login
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest("User is null!");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return BadRequest("Password is wrong!");                

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],                    
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenAsString);
        }

        return BadRequest("Some properties are not valid");
    }
}
