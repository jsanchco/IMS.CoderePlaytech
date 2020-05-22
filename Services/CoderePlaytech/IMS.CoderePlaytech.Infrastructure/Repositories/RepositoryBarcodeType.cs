namespace IMS.CoderePlaytech.Repositories.Infrastructure
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Infrastructure;
    using System;
    using System.Linq;

    #endregion

    public class RepositoryBarcodeType : IRepositoryBarcodeType, IDisposable
    {
        private readonly EFContextSQL _context;

        public RepositoryBarcodeType(EFContextSQL context)
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

        public IQueryable<BarcodeType> GetAll()
        {
            return _context.BarcodeTypes;
        }

        public BarcodeType GetById(int id)
        {
            return _context.BarcodeTypes
                 .FirstOrDefault(x => x.Id == id);
        }
    }
}
