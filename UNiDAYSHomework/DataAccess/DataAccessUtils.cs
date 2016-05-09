using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UNiDAYSHomework.Utilities
{
    public class DataAccessUtils
    {
        //abstracted to be more generic - now takes a query and dictionary of params, can be reused for other DB queries
        public static int ExecuteDbQuery(string query, Dictionary<string, object> queryParams)
        {
            int effectedLines;

            //using ensures that the connection and command are disposed of even if an exception occurs
            //this is due to SqlConnection and SqlCommand both implementing the IDisposable interface
            using (
                SqlConnection con =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["UNiDAYSDB"].ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                con.Open();

                cmd.CommandText = query;

                //parameterize values to prevent SQL injections

                foreach (KeyValuePair<string, object> entry in queryParams)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }

                effectedLines = cmd.ExecuteNonQuery();
                con.Close();
            }

            return effectedLines;
        }

    }
}