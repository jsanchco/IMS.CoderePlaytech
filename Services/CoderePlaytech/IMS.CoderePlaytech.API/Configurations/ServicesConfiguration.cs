namespace IMS.CoderePlaytech.API.Configurations
{
    #region Using

    using IMS.CoderePlaytech.Domain.Repositories;
    using IMS.CoderePlaytech.Domain.Services;
    using IMS.CoderePlaytech.Infrastructure.Repositories;
    using IMS.CoderePlaytech.Repositories.Infrastructure;
    using IMS.CoderePlaytech.Services.ServiceBarcode;
    using IMS.CoderePlaytech.WebApi.Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System.Text;

    #endregion

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IRepositoryBarcode, RepositoryBarcode>()
                .AddScoped<IRepositoryBarcodeType, RepositoryBarcodeType>()
                .AddScoped<IRepositoryBarcodeState, RepositoryBarcodeState>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services
                .AddScoped<IServiceBarcode, ServiceBarcode>();

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = new ReferenceLoopHandling());

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<JwtAppSettings>(jwtSection);

            // configure jwt authentication
            var jwtAppSettings = jwtSection.Get<JwtAppSettings>();
            var key = Encoding.ASCII.GetBytes(jwtAppSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build());
            });
    }
}