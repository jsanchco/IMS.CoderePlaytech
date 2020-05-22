namespace IMS.CoderePlaytech.Domain.Models
{
    public class ResultTransaction<T>
    {
        public T Item { get; set; }
        public bool Result { get; set; }
    }
}
