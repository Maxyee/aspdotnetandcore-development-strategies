using jwtauthcore.Controllers;
using jwtauthcore.ResponseModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jwtauthcore.Helpers
{
    public class TokenRefresher : ITokenRefresher
    {
        private readonly string key;
        public TokenRefresher(string key)
        {
            this.key = key;
        }
        public AuthenticationResponse Refresh(RefreshCred refreshCred){
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(refreshCred.JwtToken,
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                });
        }
    }
}