using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba5.Models
{
    public class Estadisticas
    {
        public string recorrido { get; set; }
        public string paradero { get; set; }
        public int intervaloHora { get; set; }
        public int rangoFecha { get; set; }
        public int rangoHora { get; set; }
        public string Hora { get; set; }
        public DatosRestriccion datos { get; set; }

        private IEnumerable<Paradero> ObtenerParaderos(TranSapoContext db)
        {
            var query = from rp in db.recorridosParadero.OrderBy(r => r.NumeroParada)
                        select rp.Paradero;
            return query;

        }
        private IEnumerable<Recorrido> ObtenerRecorridos(string codigo, TranSapoContext db)
        {
            if (codigo != null)
            {
                var query = from rp in db.recorridosParadero
                            where rp.Paradero.codigo == codigo
                            select rp.Recorrido;
                return query;
            }
            return null;
        }
        private IEnumerable<Recorrido> ObtenerRecorridos(TranSapoContext db)
        {
            var query = from rp in db.recorridosParadero
                        select rp.Recorrido;
            return query;

        }

        public void armarRestricciones()
        {
            String s = DateTime.Now.Date.ToString("dd-MM-yyyy");
            DateTime hora = DateTime.ParseExact(s+" "+ Hora+ ":00", "dd-M-yyyy HH:mm:ss",null);
            int interval = rangoHora * 2 / intervaloHora; //cant de intervaolos
           
            hora =  hora.Subtract(new TimeSpan(0,rangoHora,0));
            List<DateTime> horasProbar = new List<DateTime>();
            datos = new DatosRestriccion();
            int i = 0;
            int j = 0;
            datos.horas = new List<string>();
            while (j < rangoFecha)
            {
                hora = hora.Subtract(new TimeSpan(j, 0, 0, 0));

                while (i <= interval)
                {
                    horasProbar.Add(hora.AddMinutes(intervaloHora * i));
                    datos.horas.Add(hora.AddMinutes(intervaloHora * i).TimeOfDay.ToString());
                    i++;
                }
                j++;
                i = 0;
            }
            datos.paradero = paradero;
            datos.recorrido = recorrido;
            datos.fechas = horasProbar;
            datos.cantConsultasxDia = interval;

        }

    }
    public class DatosRestriccion
    {
        public string recorrido { get; set; }
        public string paradero { get; set; }
        public List<DateTime> fechas { get; set; }
        public int cantConsultasxDia {get;set;}
        public List<string> horas { get; set; }
    }

    public class ResultadosEstadisticas
    {
        public string recorrido { get; set; }
        public string paradero { get; set; }
        public int[,] cantidad { get; set; }
        public List<string> horas { get; set; }
    }
}