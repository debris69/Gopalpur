using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Gopalpur.Models;
using Connection_Class;
namespace Gopalpur.Controllers
{
    public class UrlController : Controller
    {
        database_Access_layer.db dblayer = new database_Access_layer.db();

        public ActionResult Index(string id)
        {

            DataSet ds = dblayer.get_category();
            ViewBag.category = ds.Tables[0];
            ViewBag.temp = id;
            String str = "";
                    Connection_Query obj1 = new Connection_Query();
                    obj1.OpenConection("Gopalpur_Tea");
                    SqlConnection connection = obj1.getSqlConnectionObject();
                    SqlDataReader rd ;
                    using (connection)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "showPost1";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(id);
                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            str = str + rd[0].ToString();
                        }
                    }
                    obj1.CloseConnection();
                    
                    ViewBag.temp = str;
            return View();
        }

        //Get submenu

        public void get_Submenu(int catid)
        {

            DataSet ds = dblayer.get_Subcategory(catid);
          
            List<SubCategory> submenulist = new List<SubCategory>();
            try
            {
               
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        submenulist.Add(new SubCategory
                        {

                            SubID = dr["SubID"].ToString(),

                            SubCategoryName = dr["SubCategoryName"].ToString(),



                        });
                    }
                }
            
            catch (Exception e)
            {

            }

            Session["submenu"] = submenulist;
        }
        //  public ActionResult Content ()



        //}
    }
}
    


