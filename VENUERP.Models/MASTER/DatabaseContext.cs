namespace VENUERP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
        public virtual DbSet<Employee> Employees { get; set; }
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrandMaster>()
                .HasMany(e => e.CategoryMasters)
                .WithRequired(e => e.BrandMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CashMaster>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationDetailItem>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationDetailItem>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationDetailItem>()
                .Property(e => e.SizeW)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationDetailItem>()
                .Property(e => e.SizeH)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationDetailItem>()
                .Property(e => e.TotSize)
                .HasPrecision(9, 2);

          

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<IQuotationMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesDetailItem>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesDetailItem>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesDetailItem>()
                .Property(e => e.SizeW)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesDetailItem>()
                .Property(e => e.SizeH)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesDetailItem>()
                .Property(e => e.TotSize)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ISalesMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.SCGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.SCGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.SSGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.SSGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.SIGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.SIGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseItemDetail>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseItemDetail>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnItemDetail>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnItemDetail>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PurchaseReturnMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationDetailItem>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationDetailItem>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationDetailItem>()
                .Property(e => e.SizeW)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationDetailItem>()
                .Property(e => e.SizeH)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationDetailItem>()
                .Property(e => e.TotSize)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<QuotationMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesDetailItem>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesDetailItem>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnDetailItem>()
                .Property(e => e.Rate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnDetailItem>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.CGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.CGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.SGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.SGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.IGSTRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.IGSTAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.TaxableAmt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesReturnMaster>()
                .Property(e => e.GrandTotal)
                .HasPrecision(9, 2);
        }
    }
}
