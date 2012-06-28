using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba5.Models
{
    public class CalificacionInformacion
    {
        public virtual int ID { get; set; }
        public virtual int? CuentaID { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        public virtual int? InformacionID { get; set; }
        public virtual Informacion Informacion { get; set; }
    }

    public class CalificacionComentario
    {
        public virtual int ID { get; set; }
        public virtual int? CuentaID { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        public virtual int? ComentarioID { get; set; }
        public virtual Comentario Comentario { get; set; }
    }

}
