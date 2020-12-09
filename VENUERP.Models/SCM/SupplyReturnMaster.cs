namespace VENUERP.Models.SCM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplyReturnMaster")]
    public partial class SupplyReturnMaster
    {
        public long id { get; set; }

        public DateTime? SupplyReturnDate { get; set; }

        public long? CustomerCode { get; set; }

        public long? SupplyReturnNo { get; set; }

        public long? ItemCode { get; set; }

        public int? Qty { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ConCode { get; set; }

        public long? UserId { get; set; }
    }
}
