using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Mvc;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using Connection_Class;
using System.Web.Script.Serialization;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting;


namespace Gopalpur.Controllers
{
    public class SubController : Controller
    {
        //
        // GET: /Sub/
        string dispdata = "<script> var availableCategories={";
        string dispdata1 = "<script> var Categories={";
        string dispdata2 = "<script> var SubCategories={";
        string dispdata3 = "<script> var  availableSubCategories={";
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["validated"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
            try
            {
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                SqlDataReader r;
                using (connection)
                {
                    
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_Cat";
                    r = cmd.ExecuteReader();
                    while(r.Read())
                    {
                        dispdata += "'" + r[0].ToString() + "':'" + r[1].ToString() + "',";
                    }
                    dispdata += "};";
                }
                dispdata = dispdata + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str = dispdata;
            try
            {
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                SqlDataReader r;
                using (connection)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_sub";
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dispdata3 += "'" + r[0].ToString() + "':'" + r[1].ToString() + "',";
                    }
                    dispdata3 += "'':''};";
                }
                dispdata3 = dispdata3 + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str2 = dispdata3;
            return View();
            }
        }
        public ActionResult DisplayData()
        {
            try
            {
                var model = new List<SubCategory>();
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                string sql1 = "select * from Gopalpur_Tea.dbo.ms_SubCategory";
                using (connection)
                {
                    
                    SqlCommand cmd = new SqlCommand(sql1,connection);
                    SqlDataReader r = cmd.ExecuteReader();
                    while(r.Read())
                    {
                        var tea = new SubCategory();
                        tea.SubID =r["SubID"].ToString();
                        tea.cat_name = r["CategoryName"].ToString();
                        tea.SubCategoryName = r["SubCategoryName"].ToString();
                        tea.active = r["Active"].ToString();
                        model.Add(tea);
                    }
                  
                }
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
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
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_Cat";
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        dispdata1 += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                    }
                    dispdata1 += "};";
                }
                dispdata1 = dispdata1 + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str1 = dispdata1;

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
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_sub";
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        dispdata2 += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                    }
                    dispdata2 += "'':''};";
                }
                dispdata2 = dispdata2 + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str3 = dispdata2;
            try
            {
                List<SubCategory> model = new List<SubCategory>();
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                string sql1 = "select * from Gopalpur_Tea.dbo.ms_SubCategory";
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand(sql1, connection);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        var tea = new SubCategory();
                        tea.SubID = r["SubID"].ToString();
                        tea.cat_name = r["CategoryName"].ToString();
                        tea.SubCategoryName = r["SubCategoryName"].ToString();
                        tea.cat_id = r["CatID"].ToString();
                        tea.active = r["Active"].ToString();
                        model.Add(tea);
                    }
                }
                SubCategory std = model.SingleOrDefault(s => s.SubID == id);
                return View(std);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
          
            return View();
        }
        [HttpPost]
        public ActionResult Edit(SubCategory ob)
        {
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
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_Cat";
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        dispdata1 += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                    }
                   dispdata1 += "};";
                }
                dispdata1 = dispdata1 + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str1 = dispdata1;

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
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_sub";
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        dispdata2 += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                    }
                    dispdata2 += "'':''};";
                }
                dispdata2 = dispdata2 + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str3 = dispdata2;
            
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
                        cmd.CommandText = "Gopalpur_Tea.dbo.UpdateData";
                        cmd.Parameters.Add("@categoryname", SqlDbType.VarChar).Value = ob.cat_name;
                        cmd.Parameters.Add("@catid", SqlDbType.VarChar).Value = ob.cat_id;
                        cmd.Parameters.Add("@subcatname", SqlDbType.VarChar).Value = ob.SubCategoryName;
                        cmd.Parameters.Add("@active", SqlDbType.VarChar).Value = ob.active;
                        cmd.Parameters.Add("@subid", SqlDbType.VarChar).Value = ob.SubID;
                        cmd.Parameters.Add("@modifyby", SqlDbType.VarChar).Value = Session["id"];
                        cmd.ExecuteNonQuery();
                       
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return RedirectToAction("DisplayData");
        }
         [HttpPost]
        public ActionResult Index(SubCategory ob)
        {
            try
            {
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                SqlDataReader r;
                using (connection)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_Cat";
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dispdata += "'" + r[0].ToString() + "':'" + r[1].ToString() + "',";
                    }
                    dispdata += "};";
                }
                dispdata = dispdata + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str = dispdata;
            try
            {
                Connection_Query obj = new Connection_Query();
                obj.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj.getSqlConnectionObject();
                SqlDataReader r;
                using (connection)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Gopalpur_Tea.dbo.get_sub";
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dispdata3 += "'" + r[0].ToString() + "':'" + r[1].ToString() + "',";
                    }
                    dispdata3 += "'':''};";
                }
                dispdata3 = dispdata3 + "</script>";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.str2 = dispdata3;
           
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
                        cmd.CommandText = "Gopalpur_Tea.dbo.InsertData";
                        cmd.Parameters.Add("@catid", SqlDbType.VarChar).Value = ob.cat_id;
                        cmd.Parameters.Add("@subcatname", SqlDbType.VarChar).Value = ob.SubCategoryName;
                        cmd.Parameters.Add("@controller", SqlDbType.VarChar).Value = ob.controller;
                        cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = ob.action;
                        cmd.Parameters.Add("@active", SqlDbType.VarChar).Value = ob.active;
                        cmd.Parameters.Add("@categoryname", SqlDbType.VarChar).Value = ob.cat_name;
                        cmd.Parameters.Add("@entryby", SqlDbType.VarChar).Value = Session["id"];
                        cmd.ExecuteNonQuery();
                        ViewBag.result = "New Sub-Category Added Successfully!";
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
           
            return View("Index");
        }
        [WebMethod]
        public void subNameExists(string name,int catid)
         {
             bool subNameInUse = false;
         
                 Connection_Query obj = new Connection_Query();
                 obj.OpenConection("Gopalpur_Tea");
                 SqlConnection connection = obj.getSqlConnectionObject();
                 using (connection)
                 {
                     SqlCommand cmd = new SqlCommand();
                     cmd.Connection = connection;
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.CommandText = "Gopalpur_Tea.dbo.get_subname";
                     cmd.Parameters.Add("@subname", SqlDbType.VarChar).Value = name;
                     cmd.Parameters.Add("@catid", SqlDbType.VarChar).Value = catid;
                     subNameInUse = Convert.ToBoolean(cmd.ExecuteScalar());
                 }
                 SubCategory ob = new SubCategory();
                 ob.SubCategoryName = name;
                 ob.cat_id = catid.ToString();
                 ob.subNameInUse = subNameInUse;

                 JavaScriptSerializer js = new JavaScriptSerializer();
                 HttpContext.Response.Write(js.Serialize(ob));
         }
    }


}