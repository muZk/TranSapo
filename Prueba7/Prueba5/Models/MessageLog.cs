using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba5.Models
{
    public static class MessageLog
    {
        private static List<string> Log = new List<string>();
        private static List<string> ErrorLog = new List<string>();

        public static List<string> Get()
        {
            return Log;
        }

        public static void Add(string msg)
        {
            Log.Add(msg);
        }

        public static List<string> GetError()
        {
            return ErrorLog;
        }

        public static void AddError(string msg)
        {
            ErrorLog.Add(msg);
        }        

        public static void Reset()
        {
            Log.Clear();
            ErrorLog.Clear();
        }

    }
}