namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CashMaster")]
    public partial class CashMaster
    {
        [Key]
        public int CashId { get; set; }

        [StringLength(50)]
        public string VoucherNo { get; set; }

        [StringLength(50)]
        public string Nature { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? CustomerId { get; set; }

        public int? SupplierID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? ComCode { get; set; }
    }
}
