﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
 

namespace VENUERP.ViewModels
{
    public  class PurchaseMasterViewModel
    {
        public int BrandID { get; set; }

        public int CategoryID { get; set; }

        public int ItemID { get; set; }

        public int SupplierId { get; set; } 
        public int Qty { get; set; } 
        public decimal? Rate { get; set; } 
        public decimal? Amount { get; set; }

        public PurchaseMaster purchaseMaster { get; set; }
        public ItemMaster ItemMaster { get; set; }

        public BrandMaster BrandMaster { get; set; }

        public CategoryMaster CategoryMaster { get; set; }

        public SupplierMaster SupplierMaster { get; set; }
    }
}
