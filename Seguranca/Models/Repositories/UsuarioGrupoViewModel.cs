using App_Dominio.Component;
using App_Dominio.Entidades;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Seguranca.Models.Repositories
{
    public class UsuarioGrupoViewModel : Repository
    {
        [DisplayName("ID_Usuário")]
        public int usuarioId { get; set; }

        [DisplayName("ID_Grupo")]
        public Nullable<int> grupoId { get; set; }
        
        [DisplayName("Situação")]
        public string situacao { get; set; }

        public string nome_usuario { get; set; }

        public string descricao_grupo { get; set; }

        public string login { get; set; }


    }
}