using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using jwtauthcore.Models;
using jwtauthcore.Interface;
using Microsoft.AspNetCore.Authorization;

namespace jwtauthcore.Controllers
{

    [Authorize]                 // reference using Microsoft.AspNetCore.Authorization;
    [Route("api/controller")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }


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
    }
}