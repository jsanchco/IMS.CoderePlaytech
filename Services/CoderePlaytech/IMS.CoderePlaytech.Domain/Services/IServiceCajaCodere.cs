namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceCajaCodere
    {
        Task<ResultRequest<Barcode>> AddBalance(double amount, string username, string code);
        Task<ResultRequest<Barcode>> RemoveBalance(double amount, string username, string code);
    }
}
