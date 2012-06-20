using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba5.Models;

namespace Prueba5.Controllers
{
    public class RestriccionesController : Controller
    {
        //
        // GET: /Restricciones/

        public ActionResult Index(RestriccionesModel model)
        {
            return View();
        }

    }
}
