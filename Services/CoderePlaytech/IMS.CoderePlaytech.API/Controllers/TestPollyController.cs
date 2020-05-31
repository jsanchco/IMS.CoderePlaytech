namespace IMS.CoderePlaytech.API.Controllers
{
    #region Using

    using AutoMapper;
    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class TestPollyController : ControllerBase
    {
        private readonly IServiceBarcode _serviceBarcode;
        private readonly ILogger<BarcodeController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public TestPollyController(
         ILogger<BarcodeController> logger,
         IServiceBarcode serviceBarcode,
         IHttpClientFactory httpClientFactory,
         IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _serviceBarcode = serviceBarcode ??
                throw new ArgumentNullException(nameof(serviceBarcode));

            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("TestPolly")]
        public async Task<IActionResult> TestPolly(string value)
        {
            try
            {
                var resultRequest = await _serviceBarcode.TestPolly(value);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in TestPolly: {resultRequest.statusError}");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in TestPolly: {resultRequest.statusError}");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet("TestPolly")]
        //public async Task<string> TestPolly(string value)
        //{
        //    var url = $"https://www.c-sharpcorner.com/mytestpagefor404";

        //    var client = _httpClientFactory.CreateClient("csharpcorner");
        //    var response = await client.GetAsync(url);
        //    var result = await response.Content.ReadAsStringAsync();
        //    return result;
        //}
    }
}
