namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryMaster")]
    public partial class CategoryMaster
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        [StringLength(50)]
        public string CategoryName { get; set; }
        [DisplayName("Brand")]
        public int BrandId { get; set; }

        public int? ComCode { get; set; }

        
        public  virtual BrandMaster BrandMaster { get; set; }
    }
}
