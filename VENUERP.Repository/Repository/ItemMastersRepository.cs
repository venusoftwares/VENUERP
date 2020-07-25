using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
using VENUERP.Repository.Interface;
using VENUERP.ViewModels.ERP; 
using System.Data;
using System.Data.Entity;
 

namespace VENUERP.Repository.Repository
{
    public class ItemMastersRepository : IItemMasters
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<ItemMasterViewModel> GetItemMasterDetails()
        {
            var result = db.ItemMasters.Include(i => i.BrandMaster).Include(i => i.CategoryMaster).Select(x => new ItemMasterViewModel()
            {
                Id = x.ItemId,
                Brand = x.BrandMaster.BrandName,
                Category = x.CategoryMaster.CategoryName,               
                HsnCode = x.Dimension,
                Item = x.Description
            });
            return result;
        }
    }
}
