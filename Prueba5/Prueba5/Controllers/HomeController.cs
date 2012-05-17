using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Prueba5.Logica;

namespace Prueba5.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Home/ObetenerInformacion


        public ActionResult ObtenerInformacion(string busqueda)
        {
            if (Request.HttpMethod == "POST")
            {
                ViewData["busqueda"] = busqueda;
                return View(new Busqueda(busqueda));
            }
            return View();
        }

    }
}
