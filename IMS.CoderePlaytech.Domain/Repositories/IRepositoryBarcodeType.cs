namespace IMS.CoderePlaytech.Domain.Repositories
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using System;
    using System.Linq;

    #endregion

    public interface IRepositoryBarcodeType : IDisposable
    {
        IQueryable<BarcodeType> GetAll();
        BarcodeType GetById(int id);
    }
}
