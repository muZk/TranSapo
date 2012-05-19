using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba5.Models;

namespace Prueba5.Controllers
{
    public class IngresarInformacionController : Controller
    {
        public ActionResult CrearInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearInfo(IngresarInformacion model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string[] PR= model.ParaderoRecorrido.Split(' ');
                List<string> ListaParametros=new List<string>();

                foreach (string parametro in PR)
                {
                    ListaParametros.Add(parametro.ToUpper());
                }
                
                bool RecorridoParaderoExiste = false;
                bool RecorridoExiste = false;
                bool ParaderoExiste = false;
                int id_p=-1, id_r=-1;
                TranSapoContext db = new TranSapoContext();

                
                //Existe el paradero
                var p_existe = from Paradero p in db.Paradero
                               where ListaParametros.Contains(p.codigo.ToUpper())
                               select p;
                foreach (Paradero p in p_existe)
                {
                    ParaderoExiste = true;
                    id_p = p.ID;
                }

                //Existe el recorrido
                var r_existe = from Recorrido r in db.recorrido
                               where ListaParametros.Contains(r.numero.ToUpper())
                               select r;
                foreach (Recorrido r in r_existe)
                {
                    RecorridoExiste = true;
                    id_r = r.ID;
                }

                //Existe la relación Paradero-Recorrido
                var rp_existe = from RecorridosParadero rp in db.recorridosParadero
                                where ListaParametros.Contains(rp.Paradero.codigo.ToUpper()) &&
                                ListaParametros.Contains(rp.Recorrido.numero.ToUpper())
                                select rp;
                foreach (RecorridosParadero rp in rp_existe)
                    RecorridoParaderoExiste = true;

                if (RecorridoParaderoExiste)
                {
                    int estado=2;
                    bool encontrado = false;
                    foreach (string Estado in ListaParametros)
                    {
                        if (!encontrado)
                        {
                            switch (Estado)
                            {
                                case "PA":
                                    estado = 1;
                                    encontrado = true;
                                    break;
                                case "V":
                                    estado = 2;
                                    encontrado = true;
                                    break;
                                case "LL":
                                    estado = 3;
                                    encontrado = true;
                                    break;
                                case "D":
                                    estado = 4;
                                    encontrado = true;
                                    break;
                                case "PR":
                                    estado = 5;
                                    encontrado = true;
                                    break;
                                default:
                                    estado = 2;
                                    break;
                            }
                        }
                    }
                    Informacion tsInfo= new Informacion();
                    tsInfo.ParaderoID = id_p;
                    tsInfo.RecorridoID = id_r;
                    tsInfo.EstadoID = estado;
                    tsInfo.fecha = DateTime.Now;

                    db.Informaciones.Add(tsInfo);
                    db.SaveChanges();

                    //Cambiar esta vista por algo mejor como "Gracias por tu aporte"
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Algo no funciona bien, lanzar mensaje de error
                    if (!ParaderoExiste)
                    {
                        ModelState.AddModelError("", "No se encontró el paradero");
                    }
                    if (!RecorridoExiste)
                    {
                        ModelState.AddModelError("", "No se encontró el recorrido");
                    }
                    if (ParaderoExiste && RecorridoExiste)
                    {
                        ModelState.AddModelError("", "El Paradero especificado no está relacionado con el recorrido");
                    }
                    
                }
            }
            return View(model);
        }

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingresar()
        {
            return View();
        }
    }
}
