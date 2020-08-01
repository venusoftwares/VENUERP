namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MapPages
    {
    
        public int Id { get; set; }

        public string Pages { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; } 
       
    }
}
