using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Common
{
    public class JWT_Token
    {
        public static string GenerateJSONWebToken(string EmailId, string SecretKey)
        {
            Random rnd = new Random();
            var nonce = rnd.Next(1000, 9999).ToString();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var signCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", EmailId),
                    new Claim("Nonce",nonce)
                });

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Test",
                Audience = "Test",
                Subject = claims,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = signCred
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
