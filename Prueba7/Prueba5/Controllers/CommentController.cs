using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba5.Models;

namespace Prueba5.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult view(ResultadoBusqueda resultadoBusqueda)
        {
            TranSapoContext db = new TranSapoContext();
            var query = from Informacion i in db.Informaciones where i.ID == resultadoBusqueda.Informacion_ID select i;
            if (query.Count() > 0)
            {
                return View("view", resultadoBusqueda);
            }
            return View();
        }

        [HttpPost]
        public ActionResult view(ResultadoBusqueda resultadoBusqueda, string button)
        {
            if (ModelState.IsValid)
            {
                TranSapoContext db = new TranSapoContext();
                Informacion informacion = db.Informaciones.Find(resultadoBusqueda.Informacion_ID);
                // Comentario
                Comentario comentario = new Comentario();
                comentario.contenido = resultadoBusqueda.comentario;
                comentario.fecha = DateTime.Now;
                comentario.Cuenta = db.Cuentas.Find(resultadoBusqueda.Cuenta_ID);
                // Comentario Informacion
                ComentarioInformacion comentarioInformacion = new ComentarioInformacion();
                comentarioInformacion.calificacion = 0;
                comentarioInformacion.Comentario = comentario;
                comentarioInformacion.Informacion = informacion;
                // Guardamos
                db.comentario.Add(comentario);
                db.comentarioInformacion.Add(comentarioInformacion);
                db.SaveChanges();
            }
            return View("view", resultadoBusqueda);
        }


        [HttpPost]
        public JsonResult ValidadorComentario(string comentario)
        {
            return Json(comentario.Length > 3 && comentario.Length < 400);
        }

    }
}
