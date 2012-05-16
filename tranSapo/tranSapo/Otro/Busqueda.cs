using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tranSapo.Models;

namespace tranSapo.Otro
{

    public abstract class Busqueda
    {

        public Paradero BusquedaParadero(string codigo)
        {
            tranSapoContext db = new tranSapoContext();
            IQueryable<Paradero> paradero = db.Paradero.Where(p => p.codigo == codigo);
            int i = paradero.Count<Paradero>();
            if (paradero.Count<Paradero>()==0)
                return null;
            return paradero.First<Paradero>();
        }




    }
}