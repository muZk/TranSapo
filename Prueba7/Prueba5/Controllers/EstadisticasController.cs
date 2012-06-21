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
            
            return View("Resultado", r);
        }

        private ResultadosEstadisticas buscar(DatosRestriccion d)
        {
            ResultadosEstadisticas r = new ResultadosEstadisticas();
            TranSapoContext db = new TranSapoContext();
            List<DateTime> fechas = d.fechas;
            int consultasxdia = d.cantConsultasxDia;
            int dias = fechas.Count / consultasxdia;
            int j = 0;
            int i = 0;
            r.cantidad = new int[consultasxdia, dias];

            while (i < consultasxdia-1)
            {
                while (j < dias)
                {
                    r.cantidad[i, j] = obtenerCantidad(d.paradero, d.recorrido, fechas[i + j * consultasxdia], fechas[i + j * consultasxdia + 1], db);
                    j++;
                }
                i++;
            }
            r.horas = d.horas;
            r.cantidad = selectImportantes(r.cantidad, consultasxdia, dias);
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

        private int[,] selectImportantes(int[,] n, int consultasxdia, int dias)
        {
            int[,] nuevo = null;
            int i = 0; int j = 0;
            int sumActual = 0;
            while (i < consultasxdia - 1)
            {
                while (j < dias)
                {
                    sumActual += n[i, j];
                    j++;
                }

                sumActual /= dias;
                if (sumActual > 0)
                {
                    nuevo = addToArray(nuevo, sumActual, i);
                }
                i++; 
            }

            return nuevo;
        }

        private int[,] addToArray(int[,] n, int cant, int indice)
        {
            if (n == null)
            {
                n = new int[0, 2];
            }
            int[,] nuevo = new int[n.GetLength(0)+ 1, 2];
            int i = 0;
            for (; i < n.GetLength(0); i++)
            {
                nuevo[i, 0] = n[i, 0];
                nuevo[i, 1] = n[i, 1];
            }
            nuevo[i, 0] = cant;
            nuevo[i, 1] = indice;
            return nuevo;
        }
    }
}