namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceBarcode
    {
        Task<ResultRequest<string>> GenDepositBarcode(string user, CancellationToken ct = default);
        ResultRequest<Barcode> TestPolly(string user, string barcode);
    }
}
