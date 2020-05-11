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

