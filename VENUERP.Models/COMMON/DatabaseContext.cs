namespace VENUERP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using VENUERP.Models.SCM;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<BankMaster> BankMasters { get; set; }
        public virtual DbSet<BrandMaster> BrandMasters { get; set; }
        public virtual DbSet<CashMaster> CashMasters { get; set; }
        public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }
        public virtual DbSet<CompanyMaster> CompanyMasters { get; set; }
        public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }       
        public virtual DbSet<IQuotationDetailItem> IQuotationDetailItem { get; set; }
        public virtual DbSet<IQuotationMaster> IQuotationMaster { get; set; }
        public virtual DbSet<ISalesDetailItem> ISalesDetailItem { get; set; }
        public virtual DbSet<ISalesMaster> ISalesMaster { get; set; }
        public virtual DbSet<ItemMaster> ItemMasters { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<PurchaseItemDetail> PurchaseItemDetails { get; set; }
        public virtual DbSet<PurchaseMaster> PurchaseMasters { get; set; }
        public virtual DbSet<PurchaseReturnItemDetail> PurchaseReturnItemDetails { get; set; }
        public virtual DbSet<PurchaseReturnMaster> PurchaseReturnMasters { get; set; }
        public virtual DbSet<QuotationDetailItem> QuotationDetailItems { get; set; }
        public virtual DbSet<QuotationMaster> QuotationMasters { get; set; }
        public virtual DbSet<ReportViewTable> ReportViewTables { get; set; }
        public virtual DbSet<SalesDetailItem> SalesDetailItems { get; set; }
        public virtual DbSet<SalesMaster> SalesMasters { get; set; }
        public virtual DbSet<SalesReturnDetailItem> SalesReturnDetailItems { get; set; }
        public virtual DbSet<SalesReturnMaster> SalesReturnMasters { get; set; }
        public virtual DbSet<SupplierMaster> SupplierMasters { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<MapPages> MapPages { get; set; }
        public virtual DbSet<PermissionsRole> PermissionsRole { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        // SCM
        public virtual DbSet<MeterialMaster> MeterialMaster { get; set; }
        public virtual DbSet<MeterialRateMaster> MeterialRateMaster { get; set; }
        public virtual DbSet<SupplyMaster> SupplyMaster { get; set; }
        public virtual DbSet<SupplyReturnMaster> SupplyReturnMaster { get; set; }
        public virtual DbSet<SupplyReturnRate> SupplyReturnRate { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
