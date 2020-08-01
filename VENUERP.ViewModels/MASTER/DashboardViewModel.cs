using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VENUERP.ViewModels.ERP
{
    public class DashboardViewModel
    {
        public int TotalBrands { get; set; }
        public int TotalCategories { get; set; }
        public int TotalItems { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalQuotation { get; set; }
        public int TotalSales { get; set; }
        public int TotalPurchase { get; set; }

      


        public decimal? TodayTotalQuotationAmount { get; set; }
        public decimal? TodayTotalSalesAmount { get; set; }
        public decimal? TodayTotalPurchases { get; set; }

        public decimal? TotalCashReceived { get; set; }
        public decimal? TotalCashPayment { get; set; }


    }
    public class ShowMenuItems
    {
        public string Pages { get; set; }
    }
}
