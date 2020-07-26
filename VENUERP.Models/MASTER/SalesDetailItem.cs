namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesDetailItem")]
    public partial class SalesDetailItem
    {
        [Key]
        public int SalesDetailID { get; set; }

        public int? SalesID { get; set; }

        public int? ItemID { get; set; }

        [StringLength(50)]
        public string Dimension { get; set; }

        public int? CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public int? ComCode { get; set; }
    }
}
