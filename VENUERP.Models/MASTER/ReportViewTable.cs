namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReportViewTable")]
    public partial class ReportViewTable
    {
        [Key]
        public int sno { get; set; }

        [StringLength(50)]
        public string Purchase { get; set; }

        [StringLength(50)]
        public string Sales { get; set; }

        [StringLength(50)]
        public string PurchaseReturn { get; set; }

        [StringLength(50)]
        public string SalesReturn { get; set; }

        [StringLength(50)]
        public string Stock { get; set; }

        [StringLength(50)]
        public string Quotation { get; set; }

        [StringLength(50)]
        public string Voucher { get; set; }

        [StringLength(50)]
        public string TrialBalance { get; set; }

        [StringLength(50)]
        public string Collection { get; set; }

        [StringLength(50)]
        public string PaymentPending { get; set; }

        [StringLength(50)]
        public string BalanceSheet { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Item { get; set; }

        [StringLength(50)]
        public string Customer { get; set; }

        [StringLength(50)]
        public string Supplier { get; set; }

        public int? ComCode { get; set; }
    }
}
