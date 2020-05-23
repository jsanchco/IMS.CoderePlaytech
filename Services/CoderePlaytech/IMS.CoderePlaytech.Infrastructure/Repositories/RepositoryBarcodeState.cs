namespace IMS.CoderePlaytech.Infrastructure.Repositories
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Infrastructure;
    using System;
    using System.Linq;

    #endregion

    public class RepositoryBarcodeState : IRepositoryBarcodeState, IDisposable
    {
        private readonly EFContextSQL _context;

        public RepositoryBarcodeState(EFContextSQL context)
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

        public IQueryable<BarcodeState> GetAll()
        {
            return _context.BarcodeStates;
        }

        public BarcodeState GetById(int id)
        {
            return _context.BarcodeStates
                 .FirstOrDefault(x => x.Id == id);
        }
    }
}
