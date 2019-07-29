using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Gopalpur.Models
{
    public class url
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public DataSet get_category()
        {
            SqlCommand com = new SqlCommand("Select CatID from Gopalpur_Tea.dbo.ms_Category", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        public DataSet get_Subcategory(int catid)
        {
            SqlCommand com = new SqlCommand("Select SubID from Gopalpur_Tea.dbo.ms_SubCategory where cat_id=@catid", con);
            com.Parameters.AddWithValue("@catid", catid);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
    }
}