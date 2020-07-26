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

        public decimal? TodayTotalQuotationAmount { get; set; }
        public int TodayTotalSales { get; set; }
        public int TodayTotalPurchases { get; set; }


    }
}
