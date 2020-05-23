namespace IMS.CoderePlaytech.Domain.Repositories
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using System.Linq;

    #endregion

    public interface IRepositoryBarcodeState
    {
        IQueryable<BarcodeState> GetAll();
        BarcodeState GetById(int id);
    }
}
