namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseItemDetail")]
    public partial class PurchaseItemDetail
    {
        [Key]
        public int PurchaseDetailID { get; set; }

        public int? PurchaseID { get; set; }

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
