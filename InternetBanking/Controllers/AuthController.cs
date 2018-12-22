using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IB.Common;
using IB.Common.Helpers;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models.Enums;
using InternetBanking.Filters;
using InternetBanking.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InternetBanking.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly SecurityOptions _options;

        public AuthController(IAuthService authService, IOptions<SecurityOptions> options)
        {
            _authService = authService;
            _options = options.Value;
        }

        [HttpPost("api/login")]
        public IActionResult Login([FromBody] LoginCommand command)
        {
            var loginModel = _authService.Login(command);

            if (loginModel.LoginResult != LoginResult.Success)
            {
                return BadRequest(loginModel.LoginResult.GetStringValue());
            }

            // Now we start to create security token
            // 1. Fixing time moment
            var now = DateTime.UtcNow;

            // 2. Construct claims
            var claims = new[]
            {
                //instead of JwtRegisteredClaimNames.Sub we use "userId"
                new Claim("userId", loginModel.UserId, ClaimValueTypes.Integer32),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                new Claim("permissions", loginModel.Permissions)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));
            var lifetime = TimeSpan.FromMinutes(_options.Lifetime);

            // 3. Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(_options.JwtIssuer, _options.JwtAudience, claims, now, now.Add(lifetime),
                new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new { token = encodedJwt, expires = (int)lifetime.TotalSeconds };
            return Json(response);
        }

        [HttpPost("api/registration")]
        [CommitRequired]
        public IActionResult Registration([FromBody] RegistrationCommand registrationCommand)
        {
            var result = _authService.Registration(registrationCommand);

            if (result.RegistrationResult != RegistrationResult.Success)
            {
                return BadRequest(result.RegistrationResult);
            }

            return Ok(result.RegistrationResult.GetStringValue());
        }



        [HttpPut("api/ban/user")]
        //[RequirePermission(Permission.Admin)]
        [CommitRequired]
        public IActionResult BanUser([FromBody]BanCommand command)
        {
            var result = _authService.BanUser(command.Id, command.IsBan);

            if (result == BanResult.NotFound)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("api/confirm/email")]
        //[RequirePermission(Permission.Admin)]
        [CommitRequired]
        public IActionResult ConfirmEmail([FromBody] EmailConfirmedCommand command)
        {
            var result = _authService.ConfirmEmail(command.Id, command.IsBan);

            if (result == EmailConfirmedResult.NotFound)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
