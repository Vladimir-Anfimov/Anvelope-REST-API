using AnvelopeApi.Data;
using AnvelopeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AnvelopeApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Controllers
{
    [ApiController]
    [Route("/security")]
    public class AuthController : ControllerBase
    {
        private readonly string securityKey;
        private readonly ISecurityRepository _securityRepository;
        private readonly ITokenUtils _tokenUtils;

        public AuthController(ISecurityRepository securityRepository, IConfiguration configuration, ITokenUtils tokenUtils)
        {
            securityKey = configuration["JwtKey"];
            _securityRepository = securityRepository;
            _tokenUtils = tokenUtils;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginUser loginUser)
        {
            var dateUtilizator = _securityRepository.LoginAdmin(loginUser);
            Console.WriteLine(loginUser.username);
            Console.WriteLine(loginUser.password);
            if (dateUtilizator == null) return NotFound(new { error = "Username-ul nu este inregistrat.", type = 0 });
            else if (BCrypt.Net.BCrypt.Verify(loginUser.password.Trim().ToLower(), dateUtilizator.password))
            {
                return Ok(new { token = _tokenUtils.GenerateToken(dateUtilizator.id_user) });
            }
            return BadRequest(new { error = "Parola invalida.", type = 1 });
        }

        [HttpPost("token/valid")]
        [Authorize]
        public ActionResult<string> ValidateToken()
        {
            return Ok(new { valid = "Valid token" });
        }

    }
}
