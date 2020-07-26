namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IQuotationDetailItem")]
    public partial class IQuotationDetailItem
    {
        [Key]
        public int IQuotationDetailID { get; set; }

        public int? IQuotationID { get; set; }

        public int? ItemID { get; set; }

        [StringLength(50)]
        public string Dimension { get; set; }

        public int? CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? SizeW { get; set; }

        public decimal? SizeH { get; set; }

        public decimal? TotSize { get; set; }

        public int? ComCode { get; set; }
        public BrandMaster brandMaster { get; set; }

        public CategoryMaster CategoryMaster { get; set; }

        public ItemMaster ItemMaster { get; set; }
    }
}
