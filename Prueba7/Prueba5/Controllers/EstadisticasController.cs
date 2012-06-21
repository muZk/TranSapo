using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba5.Models;

namespace Prueba5.Controllers
{
    public class EstadisticasController : Controller
    {
        //
        // GET: /Estadisticas/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Estadisticas model)
        {
            model.armarRestricciones();
            DatosRestriccion d = model.datos;
            ResultadosEstadisticas r = buscar(d);

            return View(model);
        }

        public ResultadosEstadisticas buscar(DatosRestriccion d)
        {
            ResultadosEstadisticas r = new ResultadosEstadisticas();
            TranSapoContext db = new TranSapoContext();
            List<DateTime> fechas = d.fechas;
            int consultasxdia = d.cantConsultasxDia;
            int dias = fechas.Count / consultasxdia;
            int contadorDia = 1;
            int j = 0;
            r.cantidad = new int[fechas.Count];
            while (j < d.fechas.Count-1 && contadorDia<=dias)
            {
                for (int i = j; i < consultasxdia*contadorDia-1; i++)
                {
                    r.cantidad[j] = obtenerCantidad(d.paradero, d.recorrido, fechas[i], fechas[i + 1], db);
                    j++;
                }
                contadorDia++;
            }
            r.fechas = fechas;
            r.paradero = d.paradero;
            r.recorrido = d.recorrido;

            return r;

        }

        private int obtenerCantidad(string paradero, string recorrido, DateTime inicio, DateTime fin, TranSapoContext db)
        {
            var cant = from Informacion i in db.Informaciones
                       where i.Paradero.codigo == paradero &&
                       i.Recorrido.numero == recorrido &&
                       i.fecha >= inicio && i.fecha <= fin
                       select i;

            return cant.Count();
        }
    }
}