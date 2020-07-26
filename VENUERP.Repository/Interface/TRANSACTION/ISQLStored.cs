using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VENUERP.Repository.Interface.TRANSACTION
{
    public interface ISQLStored
    {
        DataSet mydata(int ID, string ProcedureName);
    }
}
