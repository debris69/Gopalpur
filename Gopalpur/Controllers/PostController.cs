using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using Connection_Class;

namespace Gopalpur.Controllers
{
    public class PostController : Controller
    {  
        public ActionResult Index()
        {
            string someHtml = "";
            string someHtml1 = "";
            if (Session["validated"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var dispData = "<script> var catid={ "  ;
                var dispData1 = "<script> var catid1={";
                var dispData2 = "<script> var subcatid = {";
                var dispData3 = "<script> var subcatid1={";
                var dispData4 = "<script> var subcatid2={";
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
                        cmd.CommandText = "getCatID";

                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            dispData += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                            dispData1 += "'" + rd[1].ToString() + "':'" + rd[0].ToString() + "',";
                        }
                        dispData += "'':''}; ";
                        dispData1 += "'':''};";
                    }
                    dispData = dispData + "</script>";
                    dispData1 += "</script>";
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
                        cmd.CommandText = "getSubCatID";

                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            dispData2 += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',"; 
                        }
                        dispData2 += "'':''}; ";
                    }
                    dispData2 = dispData2 + "</script>";
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
                        cmd.CommandText = "subcatname";

                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            dispData3 += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                            dispData4 += "'" + rd[1].ToString() + "':'" + rd[0].ToString() + "',";
                        }
                        dispData3 += "'':''};";
                        dispData4 += "'':''};delete catid[''];delete catid1[''];delete subcatid[''];delete subcatid1[''];delete subcatid2['']";

                    }
                    dispData3 = dispData3 + "</script>";
                    dispData4 = dispData4 + "</script>";
                }

                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
                ViewBag.mydata = dispData + dispData2 + dispData1 + dispData3 + dispData4;

                int i=1;
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
                        cmd.CommandText = "showContents";                   
                        rd = cmd.ExecuteReader();
                        while(rd.Read())
                        {
                            var temp = rd[2].ToString();
                            if (temp == "")
                                temp = "--NIL--";
                            someHtml = someHtml + "\n" + "<tr id='row" + i + "' class='rows'><td id='rowtd'>" + rd[0].ToString() + "</td>" + "<td id='rowtd'>" + rd[1].ToString() + "</td>" + "<td id='rowtd'>" + temp + "</td>" + "<td style='display:none;' id='postcontent'>" + rd[3].ToString() + "</td>" + "<td id='rowtd'>" + "<button onclick=fillform('row" + i + "'); class='btn btn-primary'>Edit</button></td>" + "<td id='rowtd'>" + "<button onclick=fillformDel('row" + i + "'); class='btn btn-danger'>Delete</button></td></tr>";
                            i++;
                        }
                    }
    
                    Connection_Query obj1 = new Connection_Query();
                    obj1.OpenConection("Gopalpur_Tea");
                    SqlConnection connection1 = obj1.getSqlConnectionObject();
                    SqlDataReader rd1;
                    using (connection1)
                    {

                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = connection1;
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "showContentsInactive";
                        rd1 = cmd1.ExecuteReader();
                        while (rd1.Read())
                        {
                            var temp1 = rd1[2].ToString();
                            if (temp1 == "")
                                temp1 = "--NIL--";
                            someHtml1 = someHtml1 + "\n" + "<tr id='row" + i + "' class='rows'><td id='rowtd'>" + rd1[0].ToString() + "</td>" + "<td id='rowtd'>" + rd1[1].ToString() + "</td>" + "<td id='rowtd'>" + temp1 + "</td>" + "<td style='display:none;' id='postcontent'>" + rd1[3].ToString() + "</td>" + "<td id='rowtd'>" + "<button onclick=fillform('row" + i + "'); class='btn btn-primary'>Edit</button></td>" + "<td id='rowtd'>" + "<button onclick=fillformDel('row" + i + "'); class='btn btn-danger'>Delete</button></td></tr>";
                            i++;
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
            ViewBag.htmlstr = someHtml;
            ViewBag.htmlstr1 = someHtml1;
                return View();
        }

        [HttpPost]
        public ActionResult Index(string path,HttpPostedFileBase postedFile,Post obj)
        {  
            var fullPath = Request.MapPath(path);
            try
            {
                if (path!=null)
                {
                    System.IO.File.Delete(fullPath);
                    ViewBag.Message1 = "Deleted !!";
                    return Index();
                }

                if (postedFile != null)
                {
                    string path1 = Server.MapPath("~/Content/img/");
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }

                    postedFile.SaveAs(path1 + Path.GetFileName(postedFile.FileName));
                    ViewBag.Message = "File uploaded successfully. Path : /Content/img/**UPLOADED FILE NAME**";
                    return Index();
                }
               if(obj.catid != null && obj.subcatid != null)
                {
                    Connection_Query obj1 = new Connection_Query();
                    obj1.OpenConection("Gopalpur_Tea");
                    SqlConnection connection = obj1.getSqlConnectionObject();

                    using (connection)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "InsertPost";
                        cmd.Parameters.Add("@CatID", SqlDbType.Int).Value = obj.catid;
                        cmd.Parameters.Add("@SubCatID", SqlDbType.Int).Value = Convert.ToInt32(obj.subcatid);
                        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = obj.post_title;
                        cmd.Parameters.Add("@cont", SqlDbType.VarChar).Value = obj.post;
                        cmd.Parameters.Add("@Active", SqlDbType.VarChar).Value = obj.active;
                        cmd.Parameters.Add("@flag", SqlDbType.Int).Value = obj.flag;
                        cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = Session["id"];
                        cmd.ExecuteNonQuery();
                        if(obj.flag == 2)
                            ViewBag.Mssg = "Deleted Sucessfully !!!";
                        else
                            ViewBag.Mssg = "Saved Sucessfully !!!";
                    }
                }
               
                else
                {
                    ViewBag.Mssg = "Error !!!";
                }
            }

            catch(UnauthorizedAccessException e)
            {
                ViewBag.Message1 = "File not found !!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewBag.Mssg = "Error !!!";
            }
            return Index();
        }
	}
}
