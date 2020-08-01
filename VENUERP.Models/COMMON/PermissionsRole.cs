namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermissionsRole")]
    public partial class PermissionsRole
    {
        public int Id { get; set; }

        public int PageId { get; set; }

        public int RoleId { get; set; }

        public bool Add { get; set; }

        public bool Edit { get; set; }

        public bool Delete { get; set; }

        public bool View { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        [ForeignKey("PageId")]
        public  MapPages MapPages { get; set; }
        [ForeignKey("RoleId")]
        public  UserRoles UserRoles { get; set; }
    }
}
