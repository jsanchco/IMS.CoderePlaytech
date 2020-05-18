namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IService
    {
        #region Authenticate

        Task<ResultRequest<UserToLoginJson>> Login(string url, CancellationToken ct = default);
        Task<ResultRequest<UserToLoginJson>> LoginInCodere(string url, string user, string password, CancellationToken ct = default);

        #endregion

        #region Barcode

        Task<ResultRequest<string>> GenDepositBarcode(string url, string user, CancellationToken ct = default);

        #endregion
    }
}
