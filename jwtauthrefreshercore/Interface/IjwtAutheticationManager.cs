using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtauthcore.ResponseModel;

namespace jwtauthcore
{
    public interface IJwtAuthenticationManager
    {
        AuthenticationResponse Authenticate(string username, string password);
    }
}