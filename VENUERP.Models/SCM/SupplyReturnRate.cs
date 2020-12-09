namespace VENUERP.Models.SCM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplyReturnRate")]
    public partial class SupplyReturnRate
    {
        public long id { get; set; }

        public long? ItemCode { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public long? SupplyCode { get; set; }

        public long? SupplyReturnCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PerDayRate { get; set; }

        public int? Qty { get; set; }

        public int? TotalDays { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UserId { get; set; }

        public long? CustomerCode { get; set; }
    }
}
