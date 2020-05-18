namespace SGI.API.Controllers
{
    #region Using

    using IMS.CoderePlaytech.Domain.Services;
    using IMS.CoderePlaytech.WebApi.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
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
        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                var codereAppSettings = _configuration
                                            .GetSection("Codere")
                                            .Get<CodereAppSettings>();
                var url = $"{codereAppSettings.Domain}{codereAppSettings.ApiBase}";

                var resultRequest = await _service.Login(url);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in Login [{resultRequest.statusDescription}]");
                    return StatusCode(resultRequest.statusCode, $"Error in Login [{resultRequest.statusDescription}]");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginInCodere")]
        public async Task<IActionResult> LoginInCodere(Login login)
        {
            try
            {
                var codereAppSettings = _configuration
                                            .GetSection("Codere")
                                            .Get<CodereAppSettings>();
                var url = $"{codereAppSettings.Domain}{codereAppSettings.ApiBase}";

                var resultRequest = await _service.LoginInCodere(url, login.username, login.password);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in Login [{resultRequest.statusDescription}]");
                    return StatusCode(resultRequest.statusCode, $"Error in Login [{resultRequest.statusDescription}]");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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