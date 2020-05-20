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

    public class CodereAppSettings
    {
        public string Domain { get; set; }
        public string ApiBase { get; set; }
        public string ApiReports { get; set; }
        public string ApiTransactions { get; set; }
        public string ConnectionString { get; set; }
        public bool IsEncrypted { get; set; }
    }
}
