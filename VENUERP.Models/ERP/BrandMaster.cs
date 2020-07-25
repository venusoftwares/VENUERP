namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrandMaster")]
    public partial class BrandMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BrandMaster()
        {
            CategoryMasters = new HashSet<CategoryMaster>();
        }

        [Key]
        public int BrandId { get; set; }
        [DisplayName("Brand")]
        [StringLength(50)]
        public string BrandName { get; set; }

        public int? ComCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryMaster> CategoryMasters { get; set; }
    }
}
