using System;


namespace jwtauthcore.ResponseModel
{
    public class AuthenticationResponse
    {
        public string JwtToken {get;set;}
        public string RefreshToken {get;set;}
    }
}