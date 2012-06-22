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
        public ActionResult comentar(string url,string comentario,int InformacionID)
        {
            if (comentario == null || comentario == "" || comentario.Trim() == "")
                MessageLog.AddError("El mensaje no puede estar vacío.");
            else if (comentario.Length < 3 || comentario.Trim().Length < 3)
                MessageLog.AddError("El mensaje debe tener mínimo 3 carácteres.");
            else if (comentario.Length > 400)
                MessageLog.AddError("El mensaje debe tener máximo 400 carácteres.");
            else if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    TranSapoContext db = new TranSapoContext();
                    Informacion i = db.Informaciones.Find(InformacionID);
                    ComentarioInformacion comentarioInformacion = new ComentarioInformacion();
                    Comentario c = new Comentario();
                    comentarioInformacion.Comentario = c;
                    comentarioInformacion.Informacion = i;
                    comentarioInformacion.calificacion = 0;
                    c.Cuenta = Cuentas.Get(User.Identity.Name, db);
                    c.fecha = DateTime.Now;
                    c.contenido = comentario;
                    db.comentario.Add(c);
                    db.comentarioInformacion.Add(comentarioInformacion);
                    db.SaveChanges();
                    MessageLog.Add("Mensaje enviado con éxito.");
                }
                else
                {
                    HomeController.Mensajes.Add("Para comentar debe iniciar sesión.");
                    RedirectToAction("Index", "Home", null);
                }
            }
            return Redirect(url);
        }

        public ActionResult EliminarComentario(int ComentarioInformacionID,string link)
        {
            TranSapoContext db = new TranSapoContext();
            ComentarioInformacion comentarioInformacion = db.comentarioInformacion.Find(ComentarioInformacionID);
            Comentario comentario = comentarioInformacion.Comentario;
            db.comentarioInformacion.Remove(comentarioInformacion);
            db.comentario.Remove(comentario);
            db.SaveChanges();
            MessageLog.Add("Su mensaje ha sido eliminado.");
            return Redirect(link);
        }

    }
}
