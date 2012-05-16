using System.Data.Entity;

namespace tranSapo.Models
{

    enum Estado { detenido, lleno, problemas, panne }

    public class tranSapoContext: DbContext
    {
        public tranSapoContext()
            : base("ApplicationServices")
        { 
        
        }
        public DbSet<Comentario> comentario { get; set; }
        public DbSet<ComentarioInformacion> comentarioInformacion { get; set; }
        public DbSet<CallesParadero> callesParadero { get; set; }
        public DbSet<Recorrido> recorrido { get; set; }
        public DbSet<RecorridosParadero> recorridosParadero { get; set; }
        public DbSet<Cuenta> cuenta { get; set; }
        public DbSet<Paradero> Paradero { get; set; }
    }
}