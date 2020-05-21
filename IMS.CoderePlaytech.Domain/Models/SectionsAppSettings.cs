namespace IMS.CoderePlaytech.WebApi.Helpers
{
    public class InfrastructureAppSettings
    {
        public string ConnectionString { get; set; }
    }

    public class JwtAppSettings
    {
        public string SecretKey { get; set; }
    }

    public class BarcodeConfig
    {
        public int ExpirationTimeSeconds { get; set; }
    }
}
