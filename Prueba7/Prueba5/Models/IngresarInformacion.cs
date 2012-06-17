using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Prueba5.Models
{
    public class IngresarInformacion
    {
        //Formulario de ingreso
        [Required(ErrorMessage="Se te olvidó ingresar el Paradero y Recorrido")]
        //(Recorrido|Paradero|Estado){1}( Recorrido| Paradero| Estado| ){1,5}
        [RegularExpression("([A-Za-z]{0,1}[0-9]{2,3}[ec]{0,1}|[A-Za-z]{1,2}[0-9]{2,4}|LL|Ll|lL|ll|PR|Pr|pR|pr|PA|Pa|pA|pa|D|d|v|V){1}( [A-Za-z]{0,1}[0-9]{2,3}[ec]{0,1}| [A-Za-z]{1,2}[0-9]{2,4}| LL| Ll| lL| ll| PR| Pr| pR| pr| PA| Pa| pA| pa| D| d| v| V| ){0,10}", ErrorMessage = "Debes seguir el formato")]
        [Display(Name="Ingreso y Búsqueda de Información",Description="Description")]   
        [StringLength(20, ErrorMessage="No se puede sobrepasar el largo máximo de 20 caracteres")]
        public virtual string ParaderoRecorrido { get; set; }

       
        //Para el tratamiento de una búsqueda
        public IQueryable<Paradero> Paraderos { get; set; }
        public IQueryable<Recorrido> Recorridos { get; set; }
        public RecorridosParadero RPIngresado { get; set; }
        public Paradero ParaderoIngresado { get; set; }
        public Recorrido RecorridoIngresado { get; set; }
        public List<ResultadoBusqueda> ResultadoBusqueda { get; set; }
        public Cuenta Cuenta { get; set; }

    }

    public class ResultadoBusqueda
    {
        public string Recorrido { get; set; }
        public int Lejania { get; set; }
        public string NombreEstado { get; set; }
        public DateTime Fecha { get; set; }
        public string Username { get; set; }
        public int Informacion_ID { get; set; }
        public int Cuenta_ID { get; set; }

        [MaxLength(400,ErrorMessage="Mensaje debe contener menos de 400 carácteres")]
        [MinLength(3,ErrorMessage="Mensaje debe contener al menos 3 carácteres")]
        [Remote("ValidadorComentario","Comment", HttpMethod = "POST", ErrorMessage = "Por favor, siga el fomato.")]
        public string comentario { get; set; }

        public string Time()
        {
            TimeSpan temp = DateTime.Now.Subtract(Fecha);
            int dias = (int)temp.Days;
            int horas = (int)temp.Hours;
            int minutos = (int)temp.Minutes;
            int segundos = (int)temp.Seconds;
            string result = "";
            if (dias > 0)
                result = string.Format("{0} días", dias);
            else if (horas > 0)
                result = string.Format("{0} horas", horas);
            else if (minutos > 0)
                result = string.Format("{0} minutos", minutos);
            else
                result = string.Format("{0} segundos", segundos);
            return result;
        }

        public string ObtenerLejania()
        {
            if (this.Lejania == -1)
            {
                return "Un Paradero Adelante";
            }
            else if (this.Lejania < -1)
            {
                return string.Format("{0} Paraderos Adelante", (-this.Lejania));
            }
            else if (this.Lejania == 0)
            {
                return "En tu Paradero";
            }
            else if (this.Lejania == 1)
            {
                return "Un Paradero Antes";
            }
            else
            {
                return string.Format("{0} Paraderos Antes", (this.Lejania));
            }
        }

        public ResultadoBusqueda(string recorrido, int lejania, string nombreestado, DateTime fecha,string username,int Cuenta_ID,int info_id)
        {
            Recorrido = recorrido;
            Lejania = lejania;
            NombreEstado = nombreestado;
            Username = username;
            Informacion_ID = info_id;
            Fecha = fecha;
            this.Cuenta_ID = Cuenta_ID;
        }

        public ResultadoBusqueda()
        {
        }

        #region Busquedas Avanzadas
        public IQueryable<ComentarioInformacion> ObtenerComentarioInformacion()
        {
            TranSapoContext db = new TranSapoContext();
            // Comentarios puros de la informacion
            var query = from ComentarioInformacion c in db.comentarioInformacion where c.InformacionID == this.Informacion_ID select c;
            return query;
        }
        #endregion

    }

}