namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using RestSharp;
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public partial class Service
    {
        public async Task<ResultRequest<string>> GenDepositBarcode(string user, CancellationToken ct = default)
        {
            var result = await _serviceCodere.CreateDeposit(user);

            return result;
        }
    }
}
