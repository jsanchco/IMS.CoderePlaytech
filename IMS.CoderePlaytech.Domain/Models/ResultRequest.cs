namespace IMS.CoderePlaytech.Domain.Models
{
    public class ResultRequest<T>
    {
        public bool isSuccessful { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public T data { get; set; }
    }
}
