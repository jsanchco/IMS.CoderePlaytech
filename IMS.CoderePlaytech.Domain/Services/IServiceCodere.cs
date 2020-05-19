namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceCodere
    {
        Task<ResultRequest<string>> CreateDeposit(string username, CancellationToken ct = default);
    }
}
