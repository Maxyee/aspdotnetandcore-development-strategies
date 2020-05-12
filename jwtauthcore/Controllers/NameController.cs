using System;
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

        [HttpGet("{id}", Name= "Get")]
        public string Get(int id)
        {
            return "value" + id;
        } 

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            return Ok();
        }
    }
}