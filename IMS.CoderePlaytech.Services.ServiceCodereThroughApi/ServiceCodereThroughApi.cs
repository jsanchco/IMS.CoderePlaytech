namespace IMS.CoderePlaytech.Services.ServiceCodereThroughApi
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Services;
    using IMS.CoderePlaytech.Services.ServiceCodereThroughApi.Helpers;
    using IMS.CoderePlaytech.WebApi.Helpers;
    using Microsoft.Extensions.Configuration;
    using RestSharp;
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class ServiceCodereThroughApi : IServiceCodere
    {
        private readonly IConfiguration _configuration;

        public ServiceCodereThroughApi(IConfiguration configuration)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<ResultRequest<string>> CreateDeposit(string username, CancellationToken ct = default)
        {
            var codereAppSettings = _configuration
                            .GetSection("Codere")
                            .Get<CodereAppSettings>();
            var url = $"{codereAppSettings.Domain}{codereAppSettings.ApiBase}";
            var parameters = $"nombre={username}";
            url = $"{url}/account/CreateDeposit?{parameters}";
            var restClient = new RestClient(url);
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Cookie", Utils.GetCookie());
            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful)
            {
                var reference = JsonSerializer.Deserialize<string>(response.Content);
                if (string.IsNullOrEmpty(reference))
                    throw new Exception("Not value to TemplateBarcode");

                var templateBarcode = await Utils.GetTemplateBarcode(reference);

                return new ResultRequest<string>
                {
                    isSuccessful = response.IsSuccessful,
                    statusCode = (int)response.StatusCode,
                    statusDescription = response.StatusDescription,
                    data = templateBarcode
                };
            }
            else
            {
                return new ResultRequest<string>
                {
                    isSuccessful = response.IsSuccessful,
                    statusCode = (int)response.StatusCode,
                    statusDescription = response.StatusDescription,
                    data = null
                };
            }
        }
    }
}
