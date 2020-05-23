namespace IMS.CoderePlaytech.Domain.Entities
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class Barcode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Amount { get; set; }
        public DateTime? RequestDate { get; set; }

        public int BarcodeTypeId { get; set; }
        [ForeignKey("BarcodeTypeId")]
        public BarcodeType BarcodeType { get; set; }

        public int BarcodeStateId { get; set; } = 1;
        [ForeignKey("BarcodeStateId")]
        public BarcodeState BarcodeState { get; set; }

    }
}
