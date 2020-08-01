namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("License")]
    public partial class License
    {
        [Key]
        public int Lsno { get; set; }

        public int? ComCode { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? FromDate { get; set; }
    }
}
