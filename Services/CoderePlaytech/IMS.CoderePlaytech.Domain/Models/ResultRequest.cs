namespace IMS.CoderePlaytech.Domain.Models
{
    public class ResultRequest<T>
    {
        public bool isSuccessful { get; set; }
        public string statusError { get; set; }
        public T data { get; set; }
    }
}
