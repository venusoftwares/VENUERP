namespace VENUERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankMaster")]
    public partial class BankMaster
    {
        [Key]
        public int BankSno { get; set; }

        public string BANK { get; set; }

        public string ACCOUNTNO { get; set; }

        public string IFSCCode { get; set; }

        public string HOLDERNAME { get; set; }

        public string PaymentTerm1 { get; set; }

        public string PaymentTerm2 { get; set; }

        public string PaymentTerm3 { get; set; }

        public int? ComCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? UserId { get; set; }
    }
}
