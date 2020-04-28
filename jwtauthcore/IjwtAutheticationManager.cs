using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace jwtauthcore
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}