using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNiDAYSHomework.DataAccess
{
    public class QueryResult<T>
    {
        public T Results { get; set; }
        public bool WasSuccessful { get; set; }
        public string Feedback { get; set; }

    }
}