namespace IMS.CoderePlaytech.Repositories.Infrastructure
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;
    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public class RepositoryBarcode : IRepositoryBarcode, IDisposable
    {
        private readonly EFContextSQL _context;

        public RepositoryBarcode(EFContextSQL context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        public async Task<ResultTransaction<Barcode>> AddAsync(Barcode newBarcode)
        {
            _context.Barcodes.Add(newBarcode);
            return new ResultTransaction<Barcode> { Item = newBarcode, Result = await _context.SaveChangesAsync() > 0 };
        }

        public bool BarcodeExists(string username, string code)
        {
            return _context.Barcodes.FirstOrDefault(x => x.Username == username && x.Code == code) != null;
        }

        public async Task<bool> DeleteAsync(string username, string code)
        {
            var toRemove = await _context.Barcodes.FirstOrDefaultAsync(x => x.Username == username && x.Code == code);
            if (toRemove == null)
                return false;

            _context.Barcodes.Remove(toRemove);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public IQueryable<Barcode> GetAll()
        {
            return _context.Barcodes
                .Include(x => x.BarcodeType);
        }

        public IQueryable<Barcode> GetByBarcodeType(int typeId)
        {
            return _context.Barcodes
                .Include(x => x.BarcodeType)
                .Where(x => x.BarcodeTypeId == typeId);
        }

        public Barcode GetByCode(string username, string code)
        {
            return _context.Barcodes
                .Include(x => x.BarcodeType)
                .FirstOrDefault(x => x.Username == username && x.Code == code);
        }

        public async Task<Barcode> GetByCodeAsync(string username, string code)
        {
            return await _context.Barcodes
                .Include(x => x.BarcodeType)
                .FirstOrDefaultAsync(x => x.Username == username && x.Code == code);
        }

        public async Task<bool> UpdateAsync(Barcode barcode)
        {
            _context.Barcodes.Update(barcode);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
