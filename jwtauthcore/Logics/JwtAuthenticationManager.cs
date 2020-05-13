using System;
using jwtauthcore.Interface;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace jwtauthcore.Logics
{
    // for fimilier with IJwtAuthenticationManager we need to call the Interface file here
    // thats why I used "using jwtauthcore.Interface;"
    public class JwtAuthenticationManager : IJwtAuthenticationManager    
    {
        // this data will come from database, now I am using dictionary data so that 
        // i do not need to implement database code right now

        // for fimilier with IDictionary we need to call the Collection Library to this file
        // thats why I used "using System.Collections.Generic;"
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"test1", "password1"},
            {"test2", "password2"}
        };

        // Making a constructor for passing the key
        private readonly string key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string username, string password)
        {
            // if there is no user and password matched from the dictionary
            // then I will return null

            // for fimilier with Linq we need to call the LINQ Library to this file
            // thats why I used "using System.Linq;"
            if(!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            // if matched with username and password
            // then I will pass the token to that user.

            // here I used JwtSecurityTokenHandler class which will handle this token. This Class can be found on the 'nuget packages Library' which is made by Microsoft
            // website link 'https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/'
            // As a result, we need to install the Library to this project because by default dotnet core framework dont implement it
            // so I need to install it from the terminal because I am doing this project from linux . Also I am using the VS code not the visual studio.

            // From terminal we need to run this command ' dotnet add package System.IdentityModel.Tokens.Jwt --version 6.5.1'
            // After installing the Library we need to call that into this file
            // thats why I used 'using System.IdentityModel.Tokens.Jwt;'
            var tokenHandler = new JwtSecurityTokenHandler();

            // As I mentioned earlier this token should be encrypted key 
            // thats why I am using this procedure.
            var tokenKey = Encoding.ASCII.GetBytes(key); // called the 'using System.Text;' for recognisiing the 'Encoding'


            // lets create the token descriptor
            // SecurityTokenDescriptor this class should load from this Library 'using Microsoft.IdentityModel.Tokens;'
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),    //ClaimsIdentity this class should load from 'using System.Security.Claims;'
                Expires = DateTime.UtcNow.AddHours(1), // our token will expire with in 1 hour
                SigningCredentials = 
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)  // called the sha Algorithm for making token
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}