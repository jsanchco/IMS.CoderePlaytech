namespace IMS.CoderePlaytech.Domain.Entities
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class BarcodeState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public ICollection<Barcode> Barcodes { get; set; } = new HashSet<Barcode>();
    }
}
