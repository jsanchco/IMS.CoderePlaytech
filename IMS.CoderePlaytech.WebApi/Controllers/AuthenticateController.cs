namespace SGI.API.Controllers
{
    #region Using

    using IMS.CoderePlaytech.Domain.Services;
    using IMS.CoderePlaytech.WebApi.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IConfiguration _configuration;    
        
        public AuthenticateController(
            ILogger<AuthenticateController> logger, 
            IService service,
            IConfiguration configuration
            )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _service = service ??
                throw new ArgumentNullException(nameof(service));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(service));
        }

        [AllowAnonymous]
        [HttpPost("LoginInCodere")]
        public async Task<IActionResult> LoginInCodere(Login login)
        {
            try
            {
                var userAuthenticate = await _service.Login(login.username, login.password);

                if (userAuthenticate == null)
                {
                    _logger.LogWarning("Error in Authenticate: username [{Username}] not registered or incorrect password", login.username);
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                return new ObjectResult(new Session
                {
                    user = userAuthenticate,
                    token = getToken(userAuthenticate.name.ToString())
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        private string getToken(string id)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var jwtAppSettings = jwtSection.Get<JwtAppSettings>();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtAppSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                //Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token));
        }
    }
}