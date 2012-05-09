using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TranSapo.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }
        public string Nombre { get; set; }
    }

    public class Paradero
    {
        public int ID { get; set; }
        public char Codigo { get; set; }
    }

    public class Cuenta
    {
        public int ID { get; set; }
        public char Email { get; set; }
        public String Facebook { get; set; }
        public String Twitter { get; set; }
        public String Nombre { get; set; }
        public char Password { get; set; }
    }

    public class RecorridoParadero
    {
        public int ID { get; set; }
        public Paradero Paradero { get; set; }
        public Recorrido Recorrido { get; set; }
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
        public Paradero Paradero { get; set; }
    }

    public class Comentario
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public Cuenta Cuenta { get; set; }
        public String Contenido { get; set; }
    }

    public class Informacion
    {
        public int ID { get; set; }
        public Estado Estado { get; set; }
        public DateTime Fecha { get; set; }
        public Paradero Paradero { get; set; }
        public Recorrido Recorrido { get; set; }
        public Cuenta Cuenta { get; set; }
    }

    public class ComentarioInformacion
    {
        public int ID { get; set; }
        public int Calificacion { get; set; }
        public Informacion InformacionComentada { get; set; }
        public Comentario Comentario { get; set; }
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

        public DbSet<Estado> Estadoes { get; set; }
    }
}