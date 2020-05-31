namespace IMS.CoderePlaytech.Services.ServiceBarcode
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Domain.Services;
    using IMS.CoderePlaytech.Services.ServiceBarcode.Helpers;
    using IMS.CoderePlaytech.WebApi.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using RestSharp;
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class ServiceBarcode : IServiceBarcode
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryBarcode _repositoryBarcode;
        private readonly IRepositoryBarcodeType _repositoryBarcodeType;
        private readonly ILogger<ServiceBarcode> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ServiceBarcode(
            IConfiguration configuration,
            IRepositoryBarcode repositoryBarcode,
            IRepositoryBarcodeType repositoryBarcodeType,
            ILogger<ServiceBarcode> logger,
            IHttpClientFactory clientFactory)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _repositoryBarcode = repositoryBarcode ??
                throw new ArgumentNullException(nameof(repositoryBarcode));
            _repositoryBarcodeType = repositoryBarcodeType ??
                throw new ArgumentNullException(nameof(repositoryBarcodeType));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _clientFactory = clientFactory ??
                throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<ResultRequest<string>> GenDepositBarcode(string user, CancellationToken ct = default)
        {
            var barcode = Utils.NewBarcode();
            while (_repositoryBarcode.BarcodeExists(user, barcode))
            {
                barcode = Utils.NewBarcode();
            }
            var expirationTimeSeconds = _configuration
                .GetSection("BarcodeConfig")
                .Get<BarcodeConfig>().ExpirationTimeSeconds;
            var result = await _repositoryBarcode.AddAsync(new Barcode 
            {
                Username = user,
                BarcodeTypeId = 1,
                Code = barcode,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddSeconds(expirationTimeSeconds),
                BarcodeStateId = 1
        });

            var templateBarcode = await Utils.GetTemplateBarcode(barcode); 
            return result.Result
                ? new ResultRequest<string> { isSuccessful = true, data = templateBarcode }
                : new ResultRequest<string> { isSuccessful = false, data = null };
        }

        //public async Task<ResultRequest<string>> TestPolly(string value)
        //{
        //    var url = $"https://localhost:44345/api/CajaCodere/Echo?value={value}";
        //    var restClient = new RestClient(url);
        //    var restRequest = new RestRequest(Method.GET);
        //    var response = await restClient.ExecuteAsync(restRequest);

        //    return response.IsSuccessful
        //        ? new ResultRequest<string> { isSuccessful = true, data = response.Content }
        //        : new ResultRequest<string> { isSuccessful = false, data = null };
        //}

        public async Task<ResultRequest<string>> TestPolly(string value)
        {
            var url = $"https://localhost:44345/api/CajaCodere/Echo?value={value}";

            //HttpClient client = _clientFactory != null ?
            //    _clientFactory.CreateClient("cajacodere") :
            //    new HttpClient();

            var client = _clientFactory.CreateClient("cajacodere");
            var response = await client.GetAsync(url);

            return response.IsSuccessStatusCode
                ? new ResultRequest<string> { isSuccessful = true, data = await response.Content.ReadAsStringAsync() }
                : new ResultRequest<string> { isSuccessful = false, data = null };
        }
    }
}
