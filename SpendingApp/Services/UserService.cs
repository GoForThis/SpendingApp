using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpendingApp.Authorization;
using SpendingApp.Entities;
using SpendingApp.Exceptions;
using SpendingApp.ModelsDTO;
namespace SpendingApp.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthorizationSettings _authorizationSettings;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher,
            AuthorizationSettings authorizationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authorizationSettings = authorizationSettings;
        }
        public void RegisterUser(RegisterDTO dto)
        {
            var newUser = new User()
            {
                Login = dto.Login,
                RoleId = dto.RoleId,
            };
            var passwordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = passwordHash;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginDTO dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Login == dto.Login);
            if (user is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.Login}"),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorizationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authorizationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authorizationSettings.JwtIssuer,
                _authorizationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
