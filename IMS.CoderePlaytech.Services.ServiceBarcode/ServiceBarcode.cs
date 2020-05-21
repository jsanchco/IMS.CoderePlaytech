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
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class ServiceBarcode : IServiceBarcode
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryBarcode _repositoryBarcode;
        private readonly IRepositoryBarcodeType _repositoryBarcodeType;

        public ServiceBarcode(
            IConfiguration configuration,
            IRepositoryBarcode repositoryBarcode,
            IRepositoryBarcodeType repositoryBarcodeType)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _repositoryBarcode = repositoryBarcode ??
                throw new ArgumentNullException(nameof(repositoryBarcode));
            _repositoryBarcodeType = repositoryBarcodeType ??
                throw new ArgumentNullException(nameof(repositoryBarcodeType));
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
                ExpirationDate = DateTime.Now.AddSeconds(expirationTimeSeconds)
            });

            return result.Result
                ? new ResultRequest<string> { isSuccessful = true, data = result.Item.Code }
                : new ResultRequest<string> { isSuccessful = false, data = null };
        }
    }
}
