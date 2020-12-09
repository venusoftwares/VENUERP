namespace VENUERP.Models.SCM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeterialMaster")]
    public partial class MeterialMaster
    {
        public long id { get; set; }

        public long? BrandCode { get; set; }

        public long? CatCode { get; set; }

        [StringLength(50)]
        public string ItemName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ConCode { get; set; }

        public long? UserId { get; set; }
    }
}
