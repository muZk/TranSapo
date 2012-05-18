using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Prueba5.Models
{
    public class Recorrido
    {
       
            public virtual int ID { get; set; }

            [Required]
            public virtual string numero { get; set; }
     
    }
}