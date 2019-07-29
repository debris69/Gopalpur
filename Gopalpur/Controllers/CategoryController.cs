using Gopalpur.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connection_Class;

namespace Gopalpur.Controllers
{
    public class CategoryController : Controller
    {
      
        public ActionResult Index()
        {
            if (Session["validated"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
            string dispData = "<script> var availablecategory={";
            string dispData1 = "<script> var availablecategory1={";
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "";
                builder.UserID = "";
                builder.Password = "";
                builder.InitialCatalog = "";
                builder.ConnectTimeout = 10000;

                Console.Write("Connecting to Sql Server");
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlDataReader rd;
                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Gopalpur_Tea.dbo.ViewData";
                    rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        dispData += "'" + rd[0].ToString() + "':'" + rd[1].ToString() + "',";
                    }
                    dispData += "};";
                }
                dispData = dispData + "</script>";
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "";
                builder.UserID = "";
                builder.Password = "";
                builder.InitialCatalog = "";
                builder.ConnectTimeout = 10000;

                Console.Write("Connecting to Sql Server");
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlDataReader rd;
                using (connection)
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Gopalpur_Tea.dbo.ViewData";
                    rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        dispData1 += "'" + rd[0].ToString() + "':'" + rd[2].ToString() + "',";
                    }
                    dispData1 += "'':''};";
                }
                dispData1 = dispData1 + "</script>";
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            ViewBag.Temp = dispData+dispData1;
            return View();
        }
        }

        [HttpPost]
        public ActionResult Index(Category obj)
        {
               
            try
            {
                Connection_Query obj1 = new Connection_Query();
                obj1.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj1.getSqlConnectionObject();
                using (connection)
                {

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Gopalpur_Tea.dbo.InsertCategory";
                    command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = obj.catname;
                    command.Parameters.Add("@Controller", SqlDbType.VarChar).Value = obj.controller;
                    command.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.action;
                    command.Parameters.Add("@Active", SqlDbType.VarChar).Value = obj.active;
                    command.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = Session["id"];
                    command.ExecuteNonQuery(); 

                }
                ViewBag.mssg = "Category Added Successfully !";
            }
            catch (Exception e)
            {
                ViewBag.mssg = "";
                Console.WriteLine(e.ToString());
            }
          
            try
            {
                Connection_Query obj1 = new Connection_Query();
                obj1.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj1.getSqlConnectionObject();

                using (connection)
                {
                    
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Gopalpur_Tea.dbo.UpdateCategory";
                    command.Parameters.Add("@updatecatname", SqlDbType.VarChar).Value = obj.newcat;
                    command.Parameters.Add("@newactive", SqlDbType.VarChar).Value = obj.newActive;
                    command.Parameters.Add("@catid", SqlDbType.VarChar).Value = obj.updatecat;
                    command.Parameters.Add("@modifyby", SqlDbType.VarChar).Value = Session["id"];
                    command.ExecuteNonQuery();
                }
            }
           
            catch (Exception e)
            {
                 Console.WriteLine(e.ToString());
            }
            
            return Index();
        }

      
        }

        
    }
    
