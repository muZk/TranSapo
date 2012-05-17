using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prueba5.Models;

namespace Prueba5.Logica
{

    public class Busqueda
    {

        private string _paradero = null;
        private string _recorrido = null;

        private List<Paradero> _paraderos = null;
        private List<Recorrido> _recorridos = null;
        private List<Informacion> _informacion = null;

        public List<Paradero> Paraderos { get { return _paraderos; } }
        public List<Recorrido> Recorridos { get { return _recorridos; } }
        public List<Informacion> Informacion { get { return _informacion; } }
        public string Codigo { get { return _paradero; } }
        public string Numero { get { return _recorrido; } }

        private RecorridosParadero _recorridoParaderoIngresado = null;

        private static string RemoverEspacios(string s)
        {
            try
            {
                while (s.IndexOf(' ') == 0)
                    s = s.Substring(1, s.Length - 1);
                while (s.IndexOf(' ') == s.Length - 1)
                    s = s.Substring(0, s.Length - 2);
                return s;
            }
            catch
            {
                return null;
            }
        }

        private static string[] ObtenerInputs(string s)
        {
            try
            {
                string[] temp = s.Split(' ');
                List<string> result = new List<string>();
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = RemoverEspacios(temp[i]);
                    if (temp[i] != null)
                        result.Add(temp[i]);
                }
                return result.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public Busqueda(string busqueda)
        {
            string[] input = ObtenerInputs(RemoverEspacios(busqueda));
            if (input != null)
            {
                int i = 0;
                // Paramos al recorrer el arreglo o al encontrar un paradero y recorrido como inputs
                while ((i < input.Length))
                {
                    if (input[i].IndexOf('P') == 0)
                        _paradero = input[i];
                    else
                        _recorrido = input[i];
                    i++;
                }
            }
            _paraderos = ObtenerParaderos();
            _recorridos = ObtenerRecorridos();
            _recorridoParaderoIngresado = ObtenerRecorridoParadero();
            _informacion = ObtenerInformacion();
        }
        /// <summary>
        /// Obtiene el RecorridosParadero asociado a _recorrido y _paradero 
        /// </summary>
        /// <returns></returns>
        private RecorridosParadero ObtenerRecorridoParadero()
        {
            if (_paradero != null & _recorrido != null)
            {
                TranSapoContext db = new TranSapoContext();
                var query = from rp in db.recorridosParadero
                            where rp.Recorrido.numero==_recorrido & rp.Paradero.codigo==_recorrido
                            select rp;

                return query.First<RecorridosParadero>();
            }
            return null;
        }

        private List<Paradero> ObtenerParaderos()
        {
            if (_recorrido != null)
            {
                TranSapoContext db = new TranSapoContext();

                var query = from rp in db.recorridosParadero
                            where rp.Recorrido.numero==_recorrido
                            select rp.Paradero;
                
                if (query.Count<Paradero>() == 0)
                    return null;
                return query.OfType<Paradero>().ToList<Paradero>();
            }
            return null;
        }

        private List<Recorrido> ObtenerRecorridos()
        {
            if (_paradero != null)
            {
                TranSapoContext db = new TranSapoContext();

                var query = from rp in db.recorridosParadero
                            where rp.Paradero.codigo == _paradero
                            select rp.Recorrido;

                if (query.Count<Recorrido>() == 0)
                    return null;
                return query.OfType<Recorrido>().ToList<Recorrido>();
            }
            return null;
        }

        /// <summary>
        /// Este metodo retorna la información de los paraderos anteriores al paradero y recorridos consultados.
        /// </summary>
        /// <returns></returns>
        private List<Informacion> ObtenerInformacion()
        {
            if (_paradero != null & _recorrido != null & _recorridoParaderoIngresado != null)
            {
           /*     TranSapoContext db = new TranSapoContext();

                // Obtenemos los recorridos anteriores
                var rc_query = from rc in db.recorridosParadero
                               where rc.Recorrido.numero == _recorrido & rc.NumeroParada<_recorridoParaderoIngresado.NumeroParada
                               select rc;
                
                // Para luego obtener su información
                IQueryable<Informacion> info = db.Informaciones.Where(i => rc_query<Paradero>.Contains(i.Paradero) & rc_query.Contains(i.Recorrido));




                var query = from i in db.Informaciones
                            where i.Recorrido.numero == _recorrido & i.
                            select i;
                if (query.Count<Informacion>() == 0)
                    return null;
                return query.OfType<Informacion>().ToList<Informacion>();
            * */
            }
            return null;
        }

    }
}