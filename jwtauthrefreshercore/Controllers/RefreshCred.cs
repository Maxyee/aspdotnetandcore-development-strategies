using System;
using System.Collections.Generic;
using System.Text;



namespace jwtauthcore.Controllers
{
    public class RefreshCred
    {
        public string JwtToken {get;  set;}

        public string RefreshToken {get;  set;}
    }
}