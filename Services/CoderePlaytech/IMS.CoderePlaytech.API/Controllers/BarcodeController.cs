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
    using System.Threading.Tasks;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly IServiceBarcode _serviceBarcode;
        private readonly ILogger<BarcodeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public BarcodeController(
             ILogger<BarcodeController> logger,
             IServiceBarcode serviceBarcode,
             IConfiguration configuration,
             IMapper mapper
            )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _serviceBarcode = serviceBarcode ??
                throw new ArgumentNullException(nameof(serviceBarcode));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("GenDepositBarcode")]
        public async Task<IActionResult> GenDepositBarcode(string user)
        {
            try
            {
                var resultRequest = await _serviceBarcode.GenDepositBarcode(user);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning("Error in GenDepositBarcode");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in GenDepositBarcode");
                }

                return Ok(resultRequest.data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("TestPolly")]
        public IActionResult TestPolly(string user, string barcode)
        {
            try
            {
                var resultRequest = _serviceBarcode.TestPolly(user, barcode);

                if (!resultRequest.isSuccessful)
                {
                    throw new Exception("Barcode not Found");
                }

                return Ok(_mapper.Map<BarcodeViewModel>(resultRequest.data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}