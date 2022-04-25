using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnvelopeApi.Utils
{
    public class TokenUtils : ITokenUtils
    {
        private readonly string securityKey;
        public TokenUtils(IConfiguration configuration)
        {
            securityKey = configuration["JwtKey"];
        }

        public int ExtractingIdUserFromToken(string BearerToken)
        {
            string[] BearerTokenSplitted = BearerToken.Split(' ');
            var handler = new JwtSecurityTokenHandler();
            var tokenData = handler.ReadJwtToken(BearerTokenSplitted[1]);
            return Int32.Parse(tokenData.Claims.First(claim => claim.Type == "id").Value);
        }

        public string GenerateToken(int idUser)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>();
            claims.Add(new Claim("id", idUser.ToString()));
            

            var token = new JwtSecurityToken(
                issuer: "tyresjob",
                audience: "tyresjob's clients",
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signingCredentials,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
