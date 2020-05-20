namespace IMS.CoderePlaytech.Services.ServiceBarcode
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Domain.Services;
    using IMS.CoderePlaytech.Services.ServiceBarcode.Helpers;
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

        public Task<ResultRequest<string>> GenDepositBarcode(string user, CancellationToken ct = default)
        {
            var barcode = Utils.NewBarcode();

            throw new Exception($"Not completed [{barcode}]");
        }
    }
}
