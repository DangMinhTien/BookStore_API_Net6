using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiNet6.Data;
using WebApiNet6.Models;
using Microsoft.IdentityModel.Tokens;

namespace WebApiNet6.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return String.Empty;
            }
            var authClaims = new Claim[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var newUser = new AppUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            return result;
        }
    }
}
