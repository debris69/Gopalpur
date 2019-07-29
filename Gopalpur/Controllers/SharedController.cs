using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Gopalpur.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/
        public ActionResult _Layout()
        {
            var someHtml = "";

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "192.168.20.49";
                builder.UserID = "sa";
                builder.Password = "InternDB";
                builder.InitialCatalog = "Gopalpur_Tea";
                SqlDataReader rd;

                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getCatName";
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        someHtml = someHtml + "\n" + "<li class='nav-item'>  @Html.ActionLink('" + rd[0].ToString() + "','Category','Index') </li>";

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            ViewBag.layout = "HELLLOOOOOOOWWWWWEEEE";
            return View();

        }
	}
}