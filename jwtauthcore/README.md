# Work Process Jwt Authentication Dot net core

## Installation
For creating WebAPI project in dotnet core, At first in our machine there should be .netcore `SDK` installed. After that we can use the `dotnet`
command to our console or bash etc. we need to run this below command :

`dotnet new webapi`

It will create our webapi project instant. Now remove the controller which created by the template. the name the file is `weatherController.cs`

For making Jwt authentication we need to create a controller and we will fire the `authenticate` method. As a result, I made a controller
called `AuthController` and there I made a `authenticate` method

### AuthController Code :

```cs
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using jwtauthcore.Models;

namespace jwtauthcore.Controllers
{

    [Route("api/controller")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"Bangladesh", "Dhaka"};
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            return Ok();
        }
    }
}


```

Final Look of the Authenticate Mehtod

```cs
[AllowAnonymous]   // everybody can call this mehtod but rest of the method is authorized.
[HttpPost("authenticate")]
public IActionResult Authenticate([FromBody] UserCred userCred)
{
    var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
    if(token == null)
    {
        return Unauthorized();
    }
    return Ok(token);
}

```

here is the authenticate method code from `JwtAuthenticationManager` file

```cs
public string Authenticate(string username, string password)
{

    if(!users.Any(u => u.Key == username && u.Value == password))
    {
        return null;
    }


    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenKey = Encoding.ASCII.GetBytes(key); 

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, username)
        }),    
        Expires = DateTime.UtcNow.AddHours(1), 
        SigningCredentials = 
            new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature) 
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

```

And the Interface `IJwtAuthenticationManager` where I mentioned our authenticate mehtod.

```cs
namespace jwtauthcore.Interface
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}

```

Finally the dependency Injection to `Startup.cs` file

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();


    var key = "this is test key";

    services.AddAuthentication(x => 
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;   
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
    }).AddJwtBearer(x => 
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
}

```

### Testing with PostMan

Before Tesing with PostMan, we need to run our project. Without running the project we can not make the request to our local project.
However, for runnign this project we need to run the below command from our bash

`dotnet run`

After running this command we will see that our project is running on our local machine with a project port number, for my machine it 
was running on the port `https://localhost:5001/`

Another thing, from bash we can understand in which port our project is running.

Now I will share two postman request. One request will be a POST request with `username` and `password` . if this two credentials are
valid then it will give us a bearer Token

`https://localhost:5001/api/auth/authenticate`
![alt text](https://github.com/Maxyee/aspdotnetcore-development-strategies/blob/master/jwtauthcore/Screenshots/authenticateRequest.png)


After getting the token, we will use that token into this GET request. As a result, it will give us the value of that get request.
if the token is invalid then the get request will not give us any value.

`https://localhost:5001/api/auth`
![alt text](https://github.com/Maxyee/aspdotnetcore-development-strategies/blob/master/jwtauthcore/Screenshots/getValue.png)

