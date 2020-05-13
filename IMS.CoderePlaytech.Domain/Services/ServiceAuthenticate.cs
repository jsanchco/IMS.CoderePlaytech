namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public partial class Service
    {
        public Task<UserViewModel> Login(string user, string password, CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
