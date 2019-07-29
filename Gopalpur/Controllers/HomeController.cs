using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Connection_Class;

namespace Gopalpur.Controllers
{
    public class HomeController : Controller
    {
        database_Access_layer.db dblayer = new database_Access_layer.db();

        public ActionResult Index(string id)
        {

            DataSet ds = dblayer.get_category();
            ViewBag.category = ds.Tables[0];
            ViewBag.temp = id;
            String dispData = "";
            try
            {
                SqlDataReader rd;
                SqlCommand cmd;
                Connection_Query obj = new Connection_Query();

                //Enter The Database Name
                obj.OpenConection("Gopalpur_Tea");

                //Enter The Stored Procedure Name and Store inside a Sqlcommand Object
                cmd = obj.CallStoredProcedures("showPost");

                //Add Parameters 
                cmd.Parameters.Add("@CatID", SqlDbType.Int).Value = 1021;
                cmd.Parameters.Add("@SubCatID", SqlDbType.Int).Value = -1;

                //Execute Reader and use Reader object to read
                rd = obj.ExecuteAndReadStoredProceure(cmd);
                while (rd.Read())
                {
                    if (rd[1].ToString().Equals("Y"))
                        dispData = dispData + "\n" + " <div class='carousel-item'>\n<div class='row content'>\n<div class='col-lg-12'>" + "\n" + rd[0].ToString() + "</div>\n</div>\n</div>";
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.HtmlStr = dispData;
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
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}