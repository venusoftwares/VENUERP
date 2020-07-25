namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int EmpSno { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public int? Age { get; set; }

        public DateTime? StartDate { get; set; }

        [StringLength(50)]
        public string StartDateString { get; set; }

        public int? Salary { get; set; }
    }
}
