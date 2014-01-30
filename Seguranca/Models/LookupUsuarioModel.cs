using App_Dominio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguranca.Models
{
    public class LookupUsuarioModel : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuarios/ListUsuarioModal";
        }
    }

    public class LookupUsuarioFiltroModel : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuarios/_ListUsuarioModal";
        }
    }

    public class LookupUsuarioAllModel : ListViewUsuariosAll
    {
        public override string action()
        {
            return "../Usuarios/ListUsuarioAllModal";
        }
    }

    public class LookupUsuarioAllFiltroModel : ListViewUsuariosAll
    {
        public override string action()
        {
            return "../Usuarios/_ListUsuarioAllModal";
        }
    }

}