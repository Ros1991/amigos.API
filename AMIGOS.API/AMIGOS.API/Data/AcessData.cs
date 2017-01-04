using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Data
{
    public static class AcessData
    {
        public static string connStr = "Server=.;Database=Futebol;Trusted_Connection=Yes;";
        public static DataSet GetData(string SQL)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            conn.Open();
            da.Fill(ds);
            conn.Close();

            return ds;
        }

        public static int ExecuteCommand(string SQL)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            DataSet ds = new DataSet();
            conn.Open();
            int ret = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return ret;
        }
    }
}