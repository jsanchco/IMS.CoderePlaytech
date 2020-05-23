namespace IMS.CoderePlaytech.Domain.Profiles
{
    #region Using

    using AutoMapper;
    using IMS.CoderePlaytech.Domain.Entities;
    using IMS.CoderePlaytech.Domain.Models;

    #endregion

    public class BarcodeProfiles : Profile
    {
        public BarcodeProfiles()
        {
            CreateMap<Barcode, BarcodeViewModel>();
            CreateMap<BarcodeViewModel, Barcode>();
        }
    }
}
