namespace IMS.CoderePlaytech.Repositories.Infrastructure
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Repositories;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public class RepositoryBarcode : IRepositoryBarcode
    {
        public Task<ResultTransaction<Barcode>> AddAsync(Barcode newBarcode)
        {
            throw new System.NotImplementedException();
        }

        public bool BarcodeExists(string username, string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(string username, string code)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Barcode> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Barcode> GetByBarcodeType(int id)
        {
            throw new System.NotImplementedException();
        }

        public Barcode GetByCode(string username, string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<Barcode> GetByCodeAsync(string username, string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(Barcode barcode)
        {
            throw new System.NotImplementedException();
        }
    }
}
