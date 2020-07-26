namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ISalesMaster")]
    public partial class ISalesMaster
    {
        [Key]
        public int ISalesID { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime ISalesDate { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        public int CustomerID { get; set; }

        [StringLength(50)]
        public string IsCash { get; set; }

        public decimal? CGSTAmt { get; set; }

        public decimal? CGSTRate { get; set; }

        public decimal? SGSTAmt { get; set; }

        public decimal? SGSTRate { get; set; }

        public decimal? IGSTRate { get; set; }

        public decimal? IGSTAmt { get; set; }

        public decimal? TaxableAmt { get; set; }

        public decimal? TotalGST { get; set; }

        public decimal? GrandTotal { get; set; }

        public int? ComCode { get; set; }
        public CustomerMaster CustomerMaster { get; set; }
    }
}
