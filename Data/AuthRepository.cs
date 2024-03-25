using PaintStockStatusAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.User.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user.IsActive == false) {
                response.Success = false;
                response.Message = "Inactive User";
            }
            else if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                //JSON Web token that contains the user right and private key the server olny knows, but no password in complicate text.
                //It's in the browser and sent to the web service with every request.
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            // Generate a random salt value(hash value) and throw it into the algorithm with the hashed password to get the final hash value.
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = 1;
            user.IsActive = true;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            response.Data = user.Username;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.User.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            //Generate claims or user info
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            //Encode the Token key in the launchSettings.json
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            //Generate Credentials using the encoded key above.
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
