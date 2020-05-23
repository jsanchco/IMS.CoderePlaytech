namespace IMS.CajaCodere.API.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Domain.Services;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;

    #endregion

    public class ServiceCajaCodere : IServiceCajaCodere
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryBarcode _repositoryBarcode;

        public ServiceCajaCodere(
            IConfiguration configuration,
            IRepositoryBarcode repositoryBarcode)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _repositoryBarcode = repositoryBarcode ??
                throw new ArgumentNullException(nameof(repositoryBarcode));
        }

        public async Task<ResultRequest<Barcode>> AddBalance(double amount, string username, string code)
        {
            if (amount <= 0)
                throw new Exception("Amount must be greather than 0");

            // ToDo: Call CajaCodere
            // ToDo: Call CajaCodere


            var barcode = _repositoryBarcode.GetByCode(username, code);
            if (barcode == null)
                throw new Exception("Barcode not Found");

            if (barcode.BarcodeStateId != 1)
            {
                return new ResultRequest<Barcode>
                {
                    isSuccessful = false,
                    statusError = "Barcode is Used",
                    data = barcode
                };
            }

            var now = DateTime.Now;
            if (barcode.ExpirationDate < now)
            {
                barcode.RequestDate = now;
                barcode.BarcodeStateId = 3;
                if ( !await _repositoryBarcode.UpdateAsync(barcode))
                    throw new Exception("Error in Update Barcode");

                var barcodeUpdated = _repositoryBarcode.GetByCode(username, code);
                if (barcodeUpdated == null)
                    throw new Exception("Error in Update Barcode");

                return new ResultRequest<Barcode>
                {
                    isSuccessful = false,
                    statusError = "Expiration Time in Barcode",
                    data = barcodeUpdated
                };
            }
            else
            {
                barcode.RequestDate = now;
                barcode.BarcodeStateId = 2;
                barcode.Amount = amount;
                if (!await _repositoryBarcode.UpdateAsync(barcode))
                    throw new Exception("Error in Update Barcode");

                var barcodeUpdated = _repositoryBarcode.GetByCode(username, code);
                if (barcodeUpdated == null)
                    throw new Exception("Error in Update Barcode");

                return new ResultRequest<Barcode>
                {
                    isSuccessful = true,
                    data = barcodeUpdated
                };
            }
        }

        public async Task<ResultRequest<Barcode>> RemoveBalance(double amount, string username, string code)
        {
            if (amount <= 0)
                throw new Exception("Amount must be greather than 0");

            // ToDo: Call CajaCodere
            // ToDo: Call CajaCodere


            var barcode = _repositoryBarcode.GetByCode(username, code);
            if (barcode == null)
                throw new Exception("Barcode not Found");

            if (barcode.BarcodeStateId != 1)
            {
                return new ResultRequest<Barcode>
                {
                    isSuccessful = false,
                    statusError = "Barcode is Used",
                    data = barcode
                };
            }

            var now = DateTime.Now;
            if (barcode.ExpirationDate < now)
            {
                barcode.RequestDate = now;
                barcode.BarcodeStateId = 3;
                if (!await _repositoryBarcode.UpdateAsync(barcode))
                    throw new Exception("Error in Update Barcode");

                var barcodeUpdated = _repositoryBarcode.GetByCode(username, code);
                if (barcodeUpdated == null)
                    throw new Exception("Error in Update Barcode");

                return new ResultRequest<Barcode>
                {
                    isSuccessful = false,
                    statusError = "Expiration Time in Barcode",
                    data = barcodeUpdated
                };
            }
            else
            {
                barcode.RequestDate = now;
                barcode.BarcodeStateId = 2;
                barcode.Amount = amount;
                if (!await _repositoryBarcode.UpdateAsync(barcode))
                    throw new Exception("Error in Update Barcode");

                var barcodeUpdated = _repositoryBarcode.GetByCode(username, code);
                if (barcodeUpdated == null)
                    throw new Exception("Error in Update Barcode");

                return new ResultRequest<Barcode>
                {
                    isSuccessful = true,
                    data = barcodeUpdated
                };
            }
        }
    }
}
