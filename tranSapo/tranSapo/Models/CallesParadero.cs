using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tranSapo.Models
{
    public class CallesParadero
    {
        public virtual int ID { get; set; }
        public virtual string Calle { get; set; }
        public virtual Paradero Paradero { get; set; }
		public virtual int ParaderoID { get; set; }
    }
}