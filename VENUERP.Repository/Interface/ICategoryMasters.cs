﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
using VENUERP.ViewModels.ERP;

namespace VENUERP.Repository.Interface
{
    public interface ICategoryMasters
    {
       IEnumerable<CategoryMasterViewModel> GetCategoryMasterDetails();
    }
}
