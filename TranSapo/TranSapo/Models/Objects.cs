using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TranSapo.Models
{
    public enum Estado { detenido, lleno, problemas, panne }

    public class Paradero
    {
        public int ID { get; set; }
        public char codigo { get; set; }
    }

    public class Cuenta
    {
        public int ID { get; set; }
        public char email { get; set; }
        public String facebook { get; set; }
        public String twitter { get; set; }
        public String Nombre { get; set; }
        public char password { get; set; }
    }

    public class RecorridoParadero
    {
        public int ID { get; set; }
        public Paradero paradero { get; set; }
        public Recorrido recorrido { get; set; }
    }

    public class Recorrido
    {
        public int ID { get; set; }
        public char numero { get; set; }
    }

    public class CallesParadero
    {
        public int ID { get; set; }
        public String Calle { get; set; }
        public Paradero paradero { get; set; }
    }

    public class Comentario
    {
        public int ID { get; set; }
        public DateTime fecha { get; set; }
        public Cuenta cuenta { get; set; }
        public String contenido { get; set; }
    }

    public class Informacion
    {
        public int ID { get; set; }
        public Estado estado { get; set; }
        public DateTime fecha { get; set; }
        public Paradero paradero { get; set; }
        public Recorrido recorrido { get; set; }
        public Cuenta cuenta { get; set; }
    }

    public class ComentarioInformacion
    {
        public int ID { get; set; }
        public int calificacion { get; set; }
        public Informacion informacionComentada { get; set; }
        public Comentario comentarioInformacion { get; set; }
    }

    public class Conexto : DbContext
    {
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<ComentarioInformacion> ComentariosInformacion { get; set; }
        public DbSet<CallesParadero> callesParadero { get; set; }
        public DbSet<Recorrido> Recorridos { get; set; }
        public DbSet<RecorridoParadero> RecorridosParadero { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Paradero> Paraderos { get; set; }

        public DbSet<Informacion> Informacions { get; set; }
    }
}