namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using AutoMapper;
    using System;

    #endregion

    public partial class Service : IService
    {
        private readonly IMapper _mapper;
        private readonly IServiceCodere _serviceCodere;

        public Service()
        {
        }

        public Service(
            IMapper mapper,
            IServiceCodere serviceCodere)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _serviceCodere = serviceCodere ??
                throw new ArgumentNullException(nameof(serviceCodere));
        }
    }
}
