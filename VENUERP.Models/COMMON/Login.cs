namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Login")]
    public partial class Login
    {
        public int LoginId { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ComCode { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public UserRoles UserRoles { get; set; }
    }
}
