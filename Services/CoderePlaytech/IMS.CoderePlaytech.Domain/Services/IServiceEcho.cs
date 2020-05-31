namespace IMS.CoderePlaytech.Domain.Services
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceEcho
    {
        Task<ResultRequest<string>> EchoWithPolly(string value);
        Task<ResultRequest<string>> EchoWithoutPolly(string value);
        Task<ResultRequest<string>> EchoWithoutPollyWithRetry(string value);
    }
}
