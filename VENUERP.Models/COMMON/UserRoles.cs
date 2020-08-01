namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRoles
    {
    
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Role { get; set; }

        public bool Status { get; set; }
    }
}
