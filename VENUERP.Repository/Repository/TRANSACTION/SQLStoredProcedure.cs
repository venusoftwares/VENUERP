using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Repository.Interface.TRANSACTION;

namespace VENUERP.Repositoy.TRANSACTION
{
    public class SQLStoredProcedure : ISQLStored
    {
        public DataSet mydata(int ID, string ProcedureName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseContext"].ToString());
                SqlCommand cmd = new SqlCommand(ProcedureName, con);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = ID;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet myrec = new DataSet();
                da.Fill(myrec);
                return myrec;
            }            
            catch (Exception e)
            {
                throw e;
            }
        }          
        
    }
}
