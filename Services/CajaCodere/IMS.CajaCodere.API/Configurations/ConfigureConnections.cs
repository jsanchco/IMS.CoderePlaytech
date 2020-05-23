namespace IMS.CajaCodere.API.Configurations
{
    #region Using

    using IMS.CoderePlaytech.Infrastructure;
    using IMS.CoderePlaytech.WebApi.Helpers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var infrastructureSection = configuration.GetSection("Infrastructure");
            services.Configure<InfrastructureAppSettings>(infrastructureSection);
            var infrastructure = infrastructureSection.Get<InfrastructureAppSettings>();

            services.AddDbContextPool<EFContextSQL>(options => options.UseSqlServer(infrastructure.ConnectionString));

            return services;
        }
    }
}