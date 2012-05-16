using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tranSapo.Models
{
    public class RecorridosParadero
    {
        public virtual int ID { get; set; }
        public virtual Paradero Paradero { get; set; }
        public virtual int ParaderoID { get; set; }
        public virtual Recorrido Recorrido { get; set; }
        public virtual int RecorridoID { get; set; }
    }
}