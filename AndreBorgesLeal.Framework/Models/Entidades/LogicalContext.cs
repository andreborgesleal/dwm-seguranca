using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AndreBorgesLeal.Framework.Models.Entidades
{
    public sealed class LogicalContext : DbContext
    {
        static LogicalContext()
        {
            Database.SetInitializer<LogicalContext>(null);
        }

        public LogicalContext()
            : base("Name=LogicalContext")
		{
		}

        public DbSet<Sessao> Sessaos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sistema> Sistemass { get; set; }
        public DbSet<Filtro> Filtros { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
    }
}
