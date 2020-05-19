namespace IMS.CoderePlaytech.WebApi.Controllers
{
    #region Using

    using IMS.CoderePlaytech.Domain.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<BarcodeController> _logger;
        private readonly IConfiguration _configuration;

        public BarcodeController(
             ILogger<BarcodeController> logger,
             IService service,
             IConfiguration configuration
            )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _service = service ??
                throw new ArgumentNullException(nameof(service));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("GenDepositBarcode")]
        public async Task<IActionResult> GenDepositBarcode(string user)
        {
            try
            {
                var resultRequest = await _service.GenDepositBarcode(user);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in GenDepositBarcode [{resultRequest.statusDescription}]");
                    return StatusCode(resultRequest.statusCode, $"Error in GenDepositBarcode [{resultRequest.statusDescription}]");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}