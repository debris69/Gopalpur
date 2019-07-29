using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Connection_Class;

namespace Gopalpur.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin obj)
        {
            string errorText = "** Invalid Credentials";
            try
            {
                Connection_Query obj1 = new Connection_Query();
                obj1.OpenConection("Gopalpur_Tea");
                SqlConnection connection = obj1.getSqlConnectionObject(); ;
                SqlDataReader rd;
                using (connection)
                {
                    
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "adminlogin";

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = obj.id;
                    cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = obj.pwd;

                    rd = cmd.ExecuteReader();

                    while(rd.Read())
                    {
                        if (rd[0].ToString() == "yes")
                        {
                            Session["validated"] = true;
                            Session["id"] = obj.id;
                            return RedirectToAction("Index", "Post/Index");
                        }
                        else
                        {
                            ViewBag.Temp = errorText;
                            return Login();
                        }
                    }

                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            ViewBag.Temp = errorText;
            return Login();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admin model)
        {
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
                    cmd.CommandText = "registerUser";
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = model.id;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = model.pwd;
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["validated"] = null;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Index", "Home");
        }
             
	}
}