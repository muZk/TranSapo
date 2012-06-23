using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Prueba5.Models;
using System.Text;

namespace Prueba5.Backup
{
    public static class XMLBackup
    {
        public static void SaveRecorrido(string numero)
        {
            TranSapoContext db = new TranSapoContext();
            var query = from Recorrido r in db.recorrido where r.numero == numero select r;
            if (query.Count() == 0)
            {
                Recorrido r = new Recorrido();
                r.numero = numero;

                db.recorrido.Add(r);
                db.SaveChanges();
                db.Detach(r);
            }
        }

        public static void SaveParadero(string codigo)
        {
            TranSapoContext db = new TranSapoContext();
            var query = from Paradero p in db.Paradero where p.codigo == codigo select p;
            if (query.Count() == 0)
            {
                Paradero p = new Paradero();
                p.codigo = codigo;

                db.Paradero.Add(p);
                db.SaveChanges();
                db.Detach(p);
            }
        }

        public static void SaveRecorridoParadero(string numero, string codigo,string numeroParada)
        {
            TranSapoContext db = new TranSapoContext();
            int NumeroParada = int.Parse(numeroParada);

            var query = from RecorridosParadero rc in db.recorridosParadero where rc.NumeroParada == NumeroParada && rc.Paradero.codigo == codigo && rc.Recorrido.numero == numero select rc;
            if (query.Count() == 0)
            {
                RecorridosParadero rcs = new RecorridosParadero();
                rcs.NumeroParada = int.Parse(numeroParada);

                var recorridos = from Recorrido r in db.recorrido where r.numero == numero select r;
                var paraderos = from Paradero p in db.Paradero where p.codigo == codigo select p;

                rcs.Paradero = paraderos.First<Paradero>();
                rcs.Recorrido = recorridos.First<Recorrido>();

                db.recorridosParadero.Add(rcs);
                db.SaveChanges();
                db.Detach(rcs);
            }
        }

        public static void SaveEstados(string nombre)
        {
            TranSapoContext db = new TranSapoContext();
            var query = from Estado estado in db.Estados where estado.NombreEstado == nombre select estado;
            if (query.Count() == 0)
            {
                Estado e = new Estado();
                e.NombreEstado = nombre;

                db.Estados.Add(e);
                db.SaveChanges();
                db.Detach(e);
            }
        }

        public static void SaveCuentas(string email,string username, string validado,string validador,string password)
        {
            TranSapoContext db = new TranSapoContext();
            var query = from Cuenta c in db.Cuentas where c.email == email && c.username == username select c;
            if (query.Count() == 0)
            {
                Cuenta cuenta = new Cuenta();
                cuenta.email = email;
                cuenta.username = username;
                cuenta.validado = bool.Parse(validado);
                cuenta.parametroValidador = validador;
                cuenta.password = password;

                db.Cuentas.Add(cuenta);
                db.SaveChanges();
                db.Detach(cuenta);
            }
        }

        public static void SaveInformacion(string estado, string paradero, string recorrido, string fecha)
        {
            TranSapoContext db = new TranSapoContext();
            DateTime Fecha = DateTime.Parse(fecha);
            var query = from Informacion i in db.Informaciones where i.Recorrido.numero == recorrido && i.Paradero.codigo == paradero && i.Estado.NombreEstado == estado && i.fecha == Fecha select i;
            if (query.Count() == 0)
            {
                Informacion i = new Informacion();
                i.fecha = Fecha;
                var recorridos = from Recorrido r in db.recorrido where r.numero == recorrido select r;
                var paraderos = from Paradero p in db.Paradero where p.codigo == paradero select p;
                var estados = from Estado e in db.Estados where e.NombreEstado == estado select e;
                var cuenta = from Cuenta c in db.Cuentas select c;
                i.Recorrido = recorridos.First<Recorrido>();
                i.Paradero = paraderos.First<Paradero>();
                i.Estado = estados.First<Estado>();
                i.Cuenta = cuenta.First<Cuenta>();

                db.Informaciones.Add(i);
                db.SaveChanges();
                db.Detach(i);
            }
        }

        public static void Load()
        {
            TranSapoContext db = new Models.TranSapoContext();
            XmlDocument doc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(@"C:\Users\felpudito\Documents\GitHub\TranSapo\Prueba7\Prueba5\Backup\XMLBackup.xml");
            doc.Load(reader);
            System.IO.StreamWriter rder = new System.IO.StreamWriter(@"C:\Users\felpudito\Documents\GitHub\TranSapo\Prueba7\Prueba5\Backup\XMLBackup.txt");
            XmlNodeList list = doc.ChildNodes[0].ChildNodes;
            foreach (XmlNode node in list)
            {
                if (node.Name == "Recorridos")
                    foreach (XmlNode n in node.ChildNodes)
                        SaveRecorrido(n.InnerText);
                else if (node.Name == "Paraderos")
                    foreach (XmlNode n in node.ChildNodes)
                        SaveParadero(n.InnerText);
                else if (node.Name == "Cuentas")
                    foreach (XmlNode n in node.ChildNodes)
                        SaveCuentas(n["Email"].InnerText, n["Username"].InnerText, n["Validado"].InnerText, n["Validador"].InnerText, n["Password"].InnerText);
                else if (node.Name == "Estados")
                    foreach (XmlNode n in node.ChildNodes)
                        SaveEstados(n.InnerText);
                else if (node.Name == "RecorridosParaderos")
                    foreach (XmlNode n in node.ChildNodes)
                        SaveRecorridoParadero(n.ChildNodes[0].InnerText, n.ChildNodes[1].InnerText, n.ChildNodes[2].InnerText);
                else if (node.Name == "Informaciones")
                    foreach (XmlNode n in node.ChildNodes)
                        SaveInformacion(n["Nombre"].InnerText, n["Codigo"].InnerText, n["Numero"].InnerText, n["Fecha"].InnerText);

            }
            rder.Close();
            reader.Close();
        }

        public static void Backup()
        {
            TranSapoContext db = new Models.TranSapoContext();
            // XML
            XmlDocument doc = new XmlDocument();
            //raiz
            var transapo = doc.CreateElement("tranSapo"); 
            doc.AppendChild(transapo);
            // childs de raiz
            var recorridos = doc.CreateElement("Recorridos");
            var paraderos = doc.CreateElement("Paraderos");
            var informaciones = doc.CreateElement("Informaciones");
            var cuentas = doc.CreateElement("Cuentas");
            var estados = doc.CreateElement("Estados");
            var recorridosParaderos = doc.CreateElement("RecorridosParaderos");
            
            foreach(Recorrido r in db.recorrido)
            {
                var recorrido = doc.CreateElement("Recorrido");
                var numero = doc.CreateElement("Numero");
                numero.AppendChild(doc.CreateTextNode(r.numero));
                recorrido.AppendChild(numero);
                recorridos.AppendChild(recorrido);
            }
            
            foreach (Paradero p in db.Paradero)
            {
                var paradero = doc.CreateElement("Paradero");
                var codigo = doc.CreateElement("Codigo");
                codigo.AppendChild(doc.CreateTextNode(p.codigo));
                paradero.AppendChild(codigo);
                paraderos.AppendChild(paradero);
            }

            foreach (Cuenta c in db.Cuentas)
            {
                var cuenta = doc.CreateElement("Cuenta");

                var email = doc.CreateElement("Email");
                email.AppendChild(doc.CreateTextNode(c.email));

                var username = doc.CreateElement("Username");
                username.AppendChild(doc.CreateTextNode(c.username));

                var validado = doc.CreateElement("Validado");
                validado.AppendChild(doc.CreateTextNode(c.validado.ToString()));

                var validador = doc.CreateElement("Validador");
                validador.AppendChild(doc.CreateTextNode(c.parametroValidador));

                var password = doc.CreateElement("Password");
                password.AppendChild(doc.CreateTextNode(c.password));

                cuenta.AppendChild(email);
                cuenta.AppendChild(username);
                cuenta.AppendChild(validado);
                cuenta.AppendChild(validado);
                cuenta.AppendChild(validador);
                cuenta.AppendChild(password);
                cuenta.AppendChild(email);

                cuentas.AppendChild(cuenta);
            }

            foreach(Estado e in db.Estados)
            {
                var estado = doc.CreateElement("Estado");

                var nombre = doc.CreateElement("Nombre");
                nombre.AppendChild(doc.CreateTextNode(e.NombreEstado));

                estado.AppendChild(nombre);
                estados.AppendChild(estado);
            }

            foreach (RecorridosParadero rc in db.recorridosParadero)
            {
                var recorridosparradero = doc.CreateElement("RecorridoParadero");
                var recorrido = doc.CreateElement("Recorrido");
                var paradero = doc.CreateElement("Paradero");
                var numeroparada = doc.CreateElement("NumeroParada");

                var codigo = doc.CreateElement("Codigo");
                codigo.AppendChild(doc.CreateTextNode(rc.Paradero.codigo));

                var numero = doc.CreateElement("Numero");
                numero.AppendChild(doc.CreateTextNode(rc.Recorrido.numero));

                var parada = doc.CreateElement("Parada");
                parada.AppendChild(doc.CreateTextNode(rc.NumeroParada+""));

                recorrido.AppendChild(numero);
                paradero.AppendChild(codigo);
                numeroparada.AppendChild(parada);

                recorridosparradero.AppendChild(recorrido);
                recorridosparradero.AppendChild(paradero);
                recorridosparradero.AppendChild(numeroparada);

                recorridosParaderos.AppendChild(recorridosparradero);
            }

            foreach (Informacion i in db.Informaciones)
            {
                var informacion = doc.CreateElement("Informacion");

                var nombre = doc.CreateElement("Nombre");
                nombre.AppendChild(doc.CreateTextNode(i.Estado.NombreEstado));

                var codigo = doc.CreateElement("Codigo");
                codigo.AppendChild(doc.CreateTextNode(i.Paradero.codigo));

                var numero = doc.CreateElement("Numero");
                numero.AppendChild(doc.CreateTextNode(i.Recorrido.numero));

                var fecha = doc.CreateElement("Fecha");
                fecha.AppendChild(doc.CreateTextNode(i.fecha.ToString()));
                
                informacion.AppendChild(nombre);
                informacion.AppendChild(codigo);
                informacion.AppendChild(numero);
                informacion.AppendChild(fecha);
                informaciones.AppendChild(informacion);
            }

            transapo.AppendChild(recorridos);
            transapo.AppendChild(paraderos);
            
            transapo.AppendChild(cuentas);
            transapo.AppendChild(estados);
            transapo.AppendChild(recorridosParaderos);
            transapo.AppendChild(informaciones);
           
            XmlTextWriter wrtr = new XmlTextWriter(@"C:\Users\muZk\Github\TranSapo\Prueba7\Prueba5\Backup\XMLBackup.xml", Encoding.UTF8);
            wrtr.Formatting = Formatting.Indented;
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
}