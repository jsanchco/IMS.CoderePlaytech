namespace SGI.API.Configurations
{
    
    #region Using

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using IMS.CoderePlaytech.WebApi.Helpers;

    #endregion

    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var infrastructureSection = configuration.GetSection("Infrastructure");
            services.Configure<Infrastructure>(infrastructureSection);
            var infrastructure = infrastructureSection.Get<Infrastructure>();

//            services.AddDbContextPool<EFContextSQL>(options => options.UseSqlServer(infrastructure.ConnectionString));

            return services;
        }
    }
}