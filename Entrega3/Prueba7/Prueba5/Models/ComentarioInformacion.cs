using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba5.Models
{
    public class ComentarioInformacion
    {
        // Salia un error de mierda xD.. esto lo soluciono:
        // http://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table

        public virtual int ID { get; set; }
        public virtual int calificacion { get; set; }
        public int MeGusta { get; set; }
        public int NoMeGusta { get; set; }
        public virtual Informacion Informacion { get; set; }
        public virtual int? InformacionID { get; set; }
        public virtual Comentario Comentario { get; set; }
        public virtual int? ComentarioID { get; set; }
        public virtual Comentario Comentado { get; set; }
        public virtual int? ComentadoID { get; set; }
    }
}