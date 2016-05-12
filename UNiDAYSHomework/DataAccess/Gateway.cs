using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace UNiDAYSHomework.DataAccess
{
    public sealed class Gateway : IGateway
    {
        readonly string _sqlConnectionString;

        public Gateway(string connectionString)
        {
            this._sqlConnectionString = connectionString;
        }

        //abstracted to be more generic - now takes a query and dictionary of params, can be reused for other DB queries
        public QueryResult<int> ExecuteDbQuery(string query, Dictionary<string, object> queryParams)
        {

            var queryResult = new QueryResult<int>();

            //using ensures that the connection and command are disposed of even if an exception occurs
            //this is due to SqlConnection and SqlCommand both implementing the IDisposable interface
            using (var con = new SqlConnection(_sqlConnectionString))
            using (var cmd = con.CreateCommand())
            {
                con.Open();

                cmd.CommandText = query;

                //parameterize values to prevent SQL injections
                foreach (var entry in queryParams)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }

                try
                {
                    queryResult.Results = cmd.ExecuteNonQuery();
                    queryResult.WasSuccessful = true;
                }
                catch (Exception e)
                {
                    queryResult.Results = -1;
                    queryResult.WasSuccessful = false;
                    queryResult.Feedback = e.Message;
                }

                con.Close();
            }

            return queryResult;
        }


        public QueryResult<List<T>> ReturnQueryResults<T>(string query, Dictionary<string, object> queryParams, Func<DbDataReader, T> returnfromReader)
        {
            var queryResult = new QueryResult<List<T>>();

            var resultList = new List<T>();

            using (var con = new SqlConnection(_sqlConnectionString))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = query;

                if (queryParams != null)
                {
                    foreach (var entry in queryParams)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                }

                try
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var t = returnfromReader(rdr);

                            resultList.Add(t);
                        }

                        queryResult.WasSuccessful = true;
                        queryResult.Results = resultList;
                    }
                }
                catch (Exception e)
                {
                    queryResult.WasSuccessful = false;
                    queryResult.Feedback = e.Message;
                }

                con.Close();
            }

            return queryResult;
        }

    }
}