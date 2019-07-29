using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Connection_Class
{
    public class Connection_Query
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        SqlConnection con;

        public void OpenConection(string DataBaseName)
        {
            try
            {
                builder.DataSource = "";
                builder.UserID = "";
                builder.Password = "";
                builder.InitialCatalog = "";
                builder.ConnectTimeout = 10000;
                con = new SqlConnection(builder.ConnectionString);
                con.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public SqlConnection getSqlConnectionObject()
        {
            return con;
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public SqlCommand CallStoredProcedures(string StoredprocedureName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredprocedureName;
            return cmd;
        }
         
        public SqlDataReader ExecuteAndReadStoredProceure(SqlCommand cmd)
        {
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            return rd;
        }

        public void ExecuteStoredProcedure(SqlCommand cmd)
        {
            cmd.ExecuteNonQuery();
        }
    }
}