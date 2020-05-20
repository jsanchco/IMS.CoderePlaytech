namespace IMS.CoderePlaytech.Services.ServiceCodereWithoutApi
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Services;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class ServiceCodere : IServiceCodere
    {
        private readonly IConfiguration _configuration;

        public ServiceCodere(IConfiguration configuration)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        public Task<ResultRequest<string>> CreateDeposit(string username, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
