namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using RestSharp;
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public partial class Service
    {
        public async Task<ResultRequest<string>> GenDepositBarcode(string url, string user, CancellationToken ct = default)
        {
            var parameters = $"nombre={user}";
            url = $"{url}/account/CreateDeposit?{parameters}";
            var restClient = new RestClient(url);
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Cookie", GetCookie());
            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful)
            {
                var reference = JsonSerializer.Deserialize<string>(response.Content);

                return new ResultRequest<string>
                {
                    isSuccessful = response.IsSuccessful,
                    statusCode = (int)response.StatusCode,
                    statusDescription = response.StatusDescription,
                    data = reference
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

        private string GetCookie()
        {
            var cookie = ".CodereApuestas=";
            return string.Concat(
                cookie,
                "5D78CA4B89C7DF3F445532305BCBCEF27A674E8A95FA029442A491DDE32BDBFC1D85F6205C9AA45A059976EBC58995D1732F9533348ED26E35ABA91BEA713676A3899F5A2F0A9431875232BD655661E651CABABCAD2AC6331B404B485BE8BF1E559F1998F746391EA0D8D2490861650BC77F9CFF324E94C5DFDC55F02A10105236483CC8BCE0CB6DAD9D088E9E0BBF18A5F6D2C6ED1EC0D03D9D1AB76FDB74AE12F1E438A62322BBB973A934381023904898A0D4BEE117D745C09EB3EC9CE0B055A2EF5A4AD9D4503A8111752A325425BD577D41F232D7A7AEE2B1E067D4CB1DEF760B167598FAB6290EE2C52981CB4C8A92172CC83BAC497BC5CD890F0845F36602986DF31AF16CCB5B2D3FDF341A6D10E594022C0EDBB123CF05115E1C2447CDC1EAB2F06CBDAEDB5D0038BFF6C684EBBEE15549FA43E66052348186C734977356BF209996CE40D2FB092C8F0537A7C866AD0717D681561FD0DA1A9A5BEB73A9D191FE7169ECEDAA2FA6C51F009B594A38C115791BCA168B48AA18BE085397978AC576BFBAA4798DE268C641CA8DFEC9378B647D89C157C9B6B366AFD269A9E26B0FE2");
        }
    }
}
