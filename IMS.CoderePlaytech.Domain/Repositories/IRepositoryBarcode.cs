namespace IMS.CoderePlaytech.Domain.Repositories
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IRepositoryBarcode : IDisposable
    {
        bool BarcodeExists(string username, string code);
        Barcode GetByCode(string username, string code);
        IQueryable<Barcode> GetAll();
        IQueryable<Barcode> GetByBarcodeType(int id);
        Task<Barcode> GetByCodeAsync(string username, string code);
        Task<ResultTransaction<Barcode>> AddAsync(Barcode newBarcode);
        Task<bool> UpdateAsync(Barcode barcode);
        Task<bool> DeleteAsync(string username, string code);
    }
}
