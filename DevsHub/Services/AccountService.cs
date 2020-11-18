using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Contracts.V1.Responses;
using DevsHub.Data;
using DevsHub.Domain;
using DevsHub.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> LoginAsync(LoginRequest request);
        Task<AuthenticationResponse> RegisterAsync(RegisterRequest request);
        Task<User> GetAccountAsync(Guid userId);
        Task<User> UpdateAccountAsync(Guid userId, UpdateAccountRequest request);
    }

    public class AccountService : IAccountService
    {
        private readonly DataContext _dataContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AccountService(DataContext dataContext, JwtSettings jwtSettings, IMapper mapper)
        {
            _dataContext = dataContext;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return null;

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return GenerateAuthenticationResult(user);
        }

        public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return null;

            CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var user = _mapper.Map<User>(request);
            var profile = _mapper.Map<UserProfile>(request);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = Role.User;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.Profile = profile;
            profile.Rating = 100;

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return GenerateAuthenticationResult(user);
        }

        public async Task<User> GetAccountAsync(Guid userId)
        {
            return await _dataContext.Users.Include(u => u.Profile).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> UpdateAccountAsync(Guid userId, UpdateAccountRequest request)
        {
            var account = await GetAccountAsync(userId);
            if (account == null)
                return null;
            if (account.Profile == null)
                return null;

            account.Profile.FirstName = request.FirstName;
            account.Profile.LastName = request.LastName;

            _dataContext.UserProfiles.Update(account.Profile);
            var updated = await _dataContext.SaveChangesAsync();
            if (updated > 0)
                return account;
            return null;
        }

        private AuthenticationResponse GenerateAuthenticationResult(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResponse
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
