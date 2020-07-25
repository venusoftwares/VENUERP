namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemMaster")]
    public partial class ItemMaster
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Dimension { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Description { get; set; }

        public int? ComCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SCGST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SCGSTRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SSGST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SSGSTRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SIGST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SIGSTRate { get; set; }
    }
}
