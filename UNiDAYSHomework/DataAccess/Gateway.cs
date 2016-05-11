using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using UNiDAYSHomework.DataAccess;
using UNiDAYSHomework.Models;

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
        public int ExecuteDbQueryWithParams(string query, Dictionary<string, object> queryParams)
        {
            int affectedLines;

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

                affectedLines = cmd.ExecuteNonQuery();
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

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var t = returnfromReader(rdr);

                        returnList.Add(t);
                    }
                }

                con.Close();
            }

            return returnList;
        }

    }
}