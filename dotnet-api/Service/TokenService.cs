using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnet_api.Interfaces;
using dotnet_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            // 1. We use Encoding because we need to turn  it into bytes, it's not going to accept it just as a regular string, bytes means instead of it jus being a full string we're going to break it up into individual little bits. SymmetricSecurityKey is what's going to be used to encrypt it in a unique way that is only specific to our server
        }
        public string CreateToken(AppUser user)
        {
            //2. We're going to put claims within our tokens, claims are basically like your passport, it would an email or username. These are things that you can use to identify the user and express what the user can and cannot do within your system. 
            var claims = new List<Claim>
            {
                // 3. Now we follow this standard
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            // 4. Choose form of encryption
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // 5. Create the token object
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // ClaimsIdentity is where you're going to store the data
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            // 6. Set a method to create the actual token
            var tokenHandler = new JwtSecurityTokenHandler();

            // 7. Pass in the token descriptor to create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 8. We have an object representation of the token, but we don't want to return the token in the form of an actual object, we want to return it in the form of a string  
            return tokenHandler.WriteToken(token);
        }
    }
}

// 9. You need to go the Program.cs and add the dependency injection
// builder.Services.AddScoped<ITokenService, TokenService>();