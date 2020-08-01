namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyMaster")]
    public partial class CompanyMaster
    {
        [Key]
        public int ComCode { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string PhoneNo { get; set; }

        public string MobileNo { get; set; }

        public string GSTNo { get; set; }

        public string PinCode { get; set; }

        public string City { get; set; }

        public string WebSite { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] Logo { get; set; }
    }
}
