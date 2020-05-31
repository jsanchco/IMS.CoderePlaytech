namespace IMS.CoderePlaytech.Services.ServiceEcho
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Services;
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class ServiceEcho : IServiceEcho
    {
        private readonly IHttpClientFactory _clientFactory;
       
        public ServiceEcho(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ??
                throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<ResultRequest<string>> EchoWithPolly(string value)
        {
            var url = $"https://localhost:44345/api/CajaCodere/Echo?value={value}";

            var client = _clientFactory.CreateClient("cajacodere");
            var response = await client.GetAsync(url);

            return response.IsSuccessStatusCode
                ? new ResultRequest<string> { isSuccessful = true, data = await response.Content.ReadAsStringAsync() }
                : new ResultRequest<string> { isSuccessful = false, data = null };
        }

        public async Task<ResultRequest<string>> EchoWithoutPolly(string value)
        {
            var url = $"https://localhost:44345/api/CajaCodere/Echo?value={value}";

            var client = new HttpClient();
            var response = await client.GetAsync(url);

            return response.IsSuccessStatusCode
                ? new ResultRequest<string> { isSuccessful = true, data = await response.Content.ReadAsStringAsync() }
                : new ResultRequest<string> { isSuccessful = false, data = null };
        }

        public async Task<ResultRequest<string>> EchoWithoutPollyWithRetry(string value)
        {
            var url = $"https://localhost:44345/api/CajaCodere/Echo?value={value}";

            var maxRetries = 3;
            var contRetry = 1;

            var client = new HttpClient();
            HttpResponseMessage response = null;
            while (contRetry <= maxRetries)
            {
                try
                {
                    response = await client.GetAsync(url);
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Now -> {DateTime.Now.ToLongTimeString()}, Retry -> {contRetry}, Exception -> {ex.Message}");

                    Thread.Sleep(15000);
                    contRetry++;                
                }
            }

            if (response == null)
                return new ResultRequest<string> { isSuccessful = false, data = null };

            return response.IsSuccessStatusCode
                ? new ResultRequest<string> { isSuccessful = true, data = await response.Content.ReadAsStringAsync() }
                : new ResultRequest<string> { isSuccessful = false, data = null };
        }
    }
}
