using System;
using Gopalpur.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using Connection_Class;

namespace Gopalpur.Controllers
{
    
    public class garden : SubCategory
    {
        public int ImageID { get; set; }
        public String Title { get; set; }
        public String ImagePath { get; set; }
        public String ImageText { get; set; }
        public String TextHeading { get; set; }

    }
    public class Garden1Controller : Controller
    {
        database_Access_layer.db dblayer = new database_Access_layer.db();

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
        public ActionResult Add(string path,Garden ob)
        {
            if (Session["validated"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
            string disp2 = "<script>var array = [";
            string disp = "<script> var imgtitle = {";
            List<garden> sliderimages1 = new List<garden>();

            try
            {            
                SqlDataReader rd;
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                
                using (connection)
                {
                    
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "spGetAllSliderImage";
                    
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        garden slider = new garden();
                        slider.ImageID = Convert.ToInt32(rd["ImageID"]);
                        slider.Title = rd["Title"].ToString();
                        slider.ImagePath = rd["ImagePath"].ToString();
                        sliderimages1.Add(slider);
                        disp2 = disp2 + "'" + slider.ImagePath + "',";
                    }
                    disp2 = disp2 + "'']</script>";
                    rd.Close();

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                SqlDataReader rd;

                using (connection)
                {
                    
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "gettitle";

                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        disp += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                    }
                    disp += "'':''}; </script>";
                    rd.Close();
                }
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.temp = disp2 ;
            ViewBag.temp2 = disp;
   
            return View();
            }
        }

        public ActionResult Delete(string path)
        {
            if(path!=null)
            {
                string path2 = Server.MapPath( path);
                System.IO.File.Delete(path2);

                try
                {
                  
                    Connection_Query obj = new Connection_Query();
                    obj.OpenConection("Gopalpur_Tea");
                    SqlConnection connection = obj.getSqlConnectionObject();
               
                    using (connection)
                    {
                       
                        SqlCommand cmd = new SqlCommand();

                        cmd.Connection = connection;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = "spDeleteAllSliderImage";

                        cmd.Parameters.AddWithValue("@ImagePath", path);

                        cmd.ExecuteNonQuery();


                    }
                }


                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return RedirectToAction("Add", "Garden1");
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase ImageFile, Garden obj)
        {
            string disp2 = "<script>var array = [";
            List<garden> sliderimages1 = new List<garden>();
            try
            {

                //SqlConnectionStringBuilder builder2 = new SqlConnectionStringBuilder();

                //builder2.DataSource = "192.168.20.46";
                //builder2.UserID = "sa";
                //builder2.Password = "InternDB";
                //builder2.InitialCatalog = "Gopalpur_Tea";
                //SqlConnection connection2 = new SqlConnection(builder2.ConnectionString);

                Connection_Query obj1 = new Connection_Query();
                obj1.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj1.getSqlConnectionObject();
                SqlDataReader rd;

                using (connection)
                {
                    

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "spGetAllSliderImage";

                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        garden slider = new garden();
                        slider.ImageID = Convert.ToInt32(rd["ImageID"]);
                        slider.Title = rd["Title"].ToString();
                        slider.ImagePath = rd["ImagePath"].ToString();
                        sliderimages1.Add(slider);
                        disp2 = disp2 + "'" + slider.ImagePath + "',";
                    }
                    disp2 = disp2 + "'']</script>";
                    rd.Close();

                }

            }



            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {

                if (ImageFile != null)
                {
                    string fileName = ImageFile.FileName;
                    
                    string path = Server.MapPath("/Image/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    ImageFile.SaveAs(path + fileName);

                    ViewBag.Message = "File uploaded successfully. Path : /Content/img/**UPLOADED FILE NAME**";

                    

                    Connection_Query obj1 = new Connection_Query();
                    obj1.OpenConection("Gopalpur_Tea");
                    SqlConnection connection = obj1.getSqlConnectionObject();
                   

                     using (connection)
                    {
                        
                        string query = "insert into [dbo].[GardenTable1] ([Title] ,[ImagePath]) values(@Title,@ImagePath)";
                        
                        SqlCommand cmd = new SqlCommand(query, connection);

                        cmd.Parameters.AddWithValue("@Title", fileName);
                        cmd.Parameters.AddWithValue("@ImagePath", "/Image/" + fileName);

                        cmd.ExecuteNonQuery();
                    }
                }
                    return RedirectToAction("Add", "Garden1"); //return Add();
                }
            

            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("Add", "Garden1");//return Add();
            }

        }
       // 
        
        public ActionResult ViewPage()
        {
            string disp = "<script>var array = [";
            DataSet ds = dblayer.get_category();
            ViewBag.category = ds.Tables[0];

            List<garden> sliderimages = new List<garden>();

            try
            {
               
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                SqlDataReader rd;

                using (connection)
                {
                    

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "spGetAllSliderImage";

                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        garden slider = new garden();
                        slider.ImageID = Convert.ToInt32(rd["ImageID"]);
                        slider.Title = rd["Title"].ToString();
                        slider.ImagePath = rd["ImagePath"].ToString();
                        
                        sliderimages.Add(slider);
                        disp = disp + "'" + slider.ImagePath + "',";
                    }
                    disp = disp + "'']</script>";
                   
                    rd.Close();

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            

            ViewBag.temp = disp;
   

            return View();
        }
      
    }
}
