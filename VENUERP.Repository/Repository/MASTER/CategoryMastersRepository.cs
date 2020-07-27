using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
using VENUERP.Repository.Interface;
using VENUERP.ViewModels.ERP;

namespace VENUERP.Repository.Repository
{
    public class CategoryMastersRepository : ICategoryMasters
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<CategoryMasterViewModel> GetCategoryMasterDetails()
        {
            var result = db.CategoryMasters.Include(x => x.BrandMaster).Select(x => new CategoryMasterViewModel()
            {
                Brand = x.BrandMaster.BrandName,
                Category = x.CategoryName,
                Id = x.CategoryId
            });
            return result;
        }


    }
}
