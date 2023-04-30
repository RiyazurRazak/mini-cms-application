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
            var tokenHandller = new JwtSecurityTokenHandler();
            var secret_key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SEED")!);
            tokenHandller.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret_key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedtoken);

            var jwtToken = (JwtSecurityToken)validatedtoken;
            var userRole = jwtToken.Claims.First(x => x.Type == "role").ToString();
            var userId = jwtToken.Claims.First(x => x.Type == "id").ToString();

            return new List<string> { userId, userRole };
        }
    }
}
