using App_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Seguranca.Models.Entidades
{
    public class ApplicationContext : App_DominioContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
