using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Prueba5.Models
{
    public class RecorridosParadero
    {
        public virtual int ID { get; set; }
        //No es necesario coloca [Required] en los siguientes
        public virtual Paradero Paradero { get; set; }
        public virtual int ParaderoID { get; set; }
        public virtual Recorrido Recorrido { get; set; }
        public virtual int RecorridoID { get; set; }
    }
}