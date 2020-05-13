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

        Task<UserViewModel> Login(string user, string password, CancellationToken ct = default);

        #endregion
    }
}
