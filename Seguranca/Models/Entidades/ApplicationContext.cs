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
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<GrupoTransacao> GrupoTransacaos { get; set; }
        public DbSet<Transacao> Transacaos { get; set; }
        public DbSet<UsuarioGrupo> UsuarioGrupos { get; set; }
        public DbSet<EmpresaSistema> EmpresaSistemas { get; set; }
        public DbSet<LogAuditoria> LogAuditorias { get; set; }
    }
}
