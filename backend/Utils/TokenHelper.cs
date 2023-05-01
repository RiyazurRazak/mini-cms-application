using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cms_api.Utils
{
    public class TokenHelper
    {
        private static string SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        private static readonly byte[] SecretKeyBytes = Encoding.ASCII.GetBytes(SecretKey);

        public static string GenerateToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim("id", userId),
                new Claim("role", role)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static List<string> DecodeToken(string token)
        {
            Console.WriteLine(token);
            var tokenHandller = new JwtSecurityTokenHandler();
            tokenHandller.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(SecretKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            }, out var validatedtoken);

            var jwtToken = (JwtSecurityToken) validatedtoken;
            var userRole = jwtToken.Claims.First(x => x.Type == "role").Value;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            return new List<string> { userId, userRole };
        }
    }
}
