namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using AutoMapper;
    using System;

    #endregion

    public partial class Service : IService
    {
        private readonly IMapper _mapper;

        public Service()
        {
        }

        public Service(
            IMapper mapper)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
    }
}
