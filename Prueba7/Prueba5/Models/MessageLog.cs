using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba5.Models
{
    public static class MessageLog
    {
        private static List<string> Log = new List<string>();

        public static List<string> Get()
        {
            return Log;
        }

        public static void Add(string msg)
        {
            Log.Add(msg);
        }

        public static void Reset()
        {
            Log.Clear();
        }

    }
}