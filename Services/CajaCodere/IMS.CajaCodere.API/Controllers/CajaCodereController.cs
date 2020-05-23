namespace IMS.CajaCodere.API.Controllers
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
    public class CajaCodereController : ControllerBase
    {
        private readonly IServiceCajaCodere _serviceCajaCodere;
        private readonly ILogger<CajaCodereController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CajaCodereController(
             ILogger<CajaCodereController> logger,
             IServiceCajaCodere serviceCajaCodere,
             IConfiguration configuration,
             IMapper mapper
            )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _serviceCajaCodere = serviceCajaCodere ??
                throw new ArgumentNullException(nameof(serviceCajaCodere));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("AddBalance")]
        public async Task<IActionResult> AddBalance(double amount, string username, string code)
        {
            try
            {
                var resultRequest = await _serviceCajaCodere.AddBalance(amount, username, code);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in AddBalance: {resultRequest.statusError}");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in AddBalance: {resultRequest.statusError}");
                }

                return Ok(_mapper.Map<BarcodeViewModel>(resultRequest.data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("RemoveBalance")]
        public async Task<IActionResult> RemoveBalance(double amount, string username, string code)
        {
            try
            {
                var resultRequest = await _serviceCajaCodere.RemoveBalance(amount, username, code);

                if (!resultRequest.isSuccessful)
                {
                    _logger.LogWarning($"Error in RemoveBalance: {resultRequest.statusError}");
                    return StatusCode(StatusCodes.Status409Conflict, $"Error in RemoveBalance: {resultRequest.statusError}");
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
