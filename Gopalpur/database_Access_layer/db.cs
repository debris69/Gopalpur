using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Connection_Class;
namespace Gopalpur.database_Access_layer
{
    public class db
    {


       
        public DataSet get_category()
        {
            Connection_Query obj1 = new Connection_Query();
            obj1.OpenConection("Gopalpur_Tea");
            SqlConnection connection = obj1.getSqlConnectionObject();

            using (connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "getflag";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

          
                return ds;
            }
            
            

        }

        public DataSet get_Subcategory(int catid)
        {
           Connection_Query obj1 = new Connection_Query();
           obj1.OpenConection("Gopalpur_Tea");
           SqlConnection connection = obj1.getSqlConnectionObject();
           DataSet ds = new DataSet();
           try
           {
               SqlCommand com = new SqlCommand("Select SubID,SubCategoryName from Gopalpur_Tea.dbo.ms_SubCategory where CatID=@catid and Active='Y'", connection);
               com.Parameters.AddWithValue("@catid", catid);
               SqlDataAdapter da = new SqlDataAdapter(com);
               
               da.Fill(ds);
              
           }
           catch (Exception e) { 
           }
           return ds;

        }
    }
}
    