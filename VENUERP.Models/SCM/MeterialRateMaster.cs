namespace VENUERP.Models.SCM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeterialRateMaster")]
    public partial class MeterialRateMaster
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectDate { get; set; }
        public long? ItemCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ItemRate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ConCode { get; set; }

        public long UserId { get; set; }
    }
}
