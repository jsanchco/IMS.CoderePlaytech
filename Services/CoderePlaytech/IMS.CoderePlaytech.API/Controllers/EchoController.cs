namespace IMS.CoderePlaytech.API.Controllers
{
    #region Using

    using AutoMapper;
    using IMS.CoderePlaytech.Domain.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        private readonly IServiceEcho _serviceEcho;
        private readonly ILogger<BarcodeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public EchoController(
            IServiceEcho serviceEcho,
            ILogger<BarcodeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _serviceEcho = serviceEcho ??
                throw new ArgumentNullException(nameof(serviceEcho));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [HttpGet("EchoWithPolly")]
        public async Task<IActionResult> EchoWithPolly(string value)
        {
            try
            {
                var resultRequest = await _serviceEcho.EchoWithPolly(value);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in EchoWithPolly: {resultRequest.statusError}");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in EchoWithPolly: {resultRequest.statusError}");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("EchoWithoutPolly")]
        public async Task<IActionResult> EchoWithoutPolly(string value)
        {
            try
            {
                var resultRequest = await _serviceEcho.EchoWithoutPolly(value);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in EchoWithoutPolly: {resultRequest.statusError}");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in EchoWithoutPolly: {resultRequest.statusError}");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("EchoWithoutPollyWithRetry")]
        public async Task<IActionResult> EchoWithoutPollyWithRetry(string value)
        {
            try
            {
                var resultRequest = await _serviceEcho.EchoWithoutPollyWithRetry(value);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in EchoWithoutPollyWithRetry: {resultRequest.statusError}");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in EchoWithoutPollyWithRetry: {resultRequest.statusError}");
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
