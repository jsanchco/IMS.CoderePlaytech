namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using RestSharp;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public partial class Service
    {

        public async Task<ResultRequest<UserToLoginJson>> Login(string url, CancellationToken ct = default)
        {
            url = string.Concat(url, "/account/login");
            var restClient = new RestClient(url);
            var restRequest = new RestRequest(Method.GET);
            //restRequest.AddHeader("cache-control", "no-cache");
            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful)
            {
                var userToLoginJson = JsonSerializer.Deserialize<UserToLoginJson>(response.Content);

                return new ResultRequest<UserToLoginJson> { 
                    isSuccessful = response.IsSuccessful, 
                    statusCode = (int)response.StatusCode,
                    statusDescription = response.StatusDescription,
                    data = userToLoginJson 
                };
            }
            else
            {
                return new ResultRequest<UserToLoginJson>
                {
                    isSuccessful = response.IsSuccessful,
                    statusCode = (int)response.StatusCode,
                    statusDescription = response.StatusDescription,
                    data = null
                };
            }
        }

        public async Task<ResultRequest<UserToLoginJson>> LoginInCodere(string url, string user, string password, CancellationToken ct = default)
        {
            var parameters = $"loginName={user}&password={password}&persistCookie=false";
            url = $"{url}/account/loginjson?{parameters}";
            var restClient = new RestClient(url);
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful)
            {
                var userToLoginJson = JsonSerializer.Deserialize<UserToLoginJson>(response.Content);

                return new ResultRequest<UserToLoginJson>
                {
                    isSuccessful = response.IsSuccessful,
                    statusCode = (int)response.StatusCode,
                    statusDescription = response.StatusDescription,
                    data = userToLoginJson
                };
            }
            else
            {
                return new ResultRequest<UserToLoginJson>
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
