﻿namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IService
    {
        #region Barcode

        Task<ResultRequest<string>> GenDepositBarcode(string user, CancellationToken ct = default);

        #endregion
    }
}
