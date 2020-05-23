namespace IMS.CoderePlaytech.Domain.Models
{
    #region Using

    using System;


    #endregion

    public class BarcodeViewModel
    {
        public int id { get; set; }
        public string username { get; set; }

        public string code { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime expirationDate { get; set; }
        public double amount { get; set; }
        public DateTime? requestDate { get; set; }

        public int barcodeTypeId { get; set; } 
        public string barcodeTypeName { get; set; }
        public int barcodeStateId { get; set; } = 1;
        public string barcodeStateDescription { get; set; }
    }
}
