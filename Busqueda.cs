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
                while ((i < input.Length) || (_paradero != null & _recorrido != null))
                {
                    if (input[i].IndexOf('P') == 1)
                        _paradero = input[i];
                    else
                        _recorrido = input[i];
                    i++;
                }
            }
        }

        public List<Paradero> ObtenerParaderos()
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

        public List<Recorrido> ObtenerRecorridos()
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

        public List<Informacion> ObtenerInformacion()
        {
            if (_paradero != null & _recorrido != null)
            {
                TranSapoContext db = new TranSapoContext();
                var query = from i in db.Informaciones
                            where i.Paradero.codigo == _paradero & i.Recorrido.numero == _recorrido
                            select i;
                if (query.Count<Informacion>() == 0)
                    return null;
                return query.OfType<Informacion>().ToList<Informacion>();
            }
            return null;
        }

    }
}