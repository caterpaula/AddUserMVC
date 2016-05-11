using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace UNiDAYSHomework.DataAccess
{
    public class Gateway : IGateway
    {

        string SQLConnectionString;

        public Gateway(string connectionString)
        {
            this.SQLConnectionString = connectionString;
        }

        //abstracted to be more generic - now takes a query and dictionary of params, can be reused for other DB queries
        public int ExecuteDbQuery(string query, Dictionary<string, object> queryParams)
        {
            int affectedLines = -1;

            //using ensures that the connection and command are disposed of even if an exception occurs
            //this is due to SqlConnection and SqlCommand both implementing the IDisposable interface
            using (var con = new SqlConnection(SQLConnectionString))
            using (var cmd = con.CreateCommand())
            {
                con.Open();

                cmd.CommandText = query;

                //parameterize values to prevent SQL injections

                foreach (KeyValuePair<string, object> entry in queryParams)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }

                try
                {
                    affectedLines = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    throw;
                }

                con.Close();
            }

            return affectedLines;
        }


        public List<T> ReturnQueryResults<T>(string query, Dictionary<string, object> queryParams, Func<DbDataReader, T> returnfromReader)
        {

            var returnList = new List<T>();

            
            using (var con = new SqlConnection(SQLConnectionString))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = query;

                foreach (KeyValuePair<string, object> entry in queryParams)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }

                try
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var t = returnfromReader(rdr);

                            returnList.Add(t);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    throw;
                }

            con.Close();
            }

            return returnList;
        }

    }
}