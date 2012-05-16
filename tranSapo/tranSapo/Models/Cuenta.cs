using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tranSapo.Models
{
    public class Cuenta
    {
        public virtual int ID { get; set; }
        public virtual string email { get; set; }
        public virtual string facebook { get; set; }
        public virtual string twitter { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string password { get; set; }
    }
}