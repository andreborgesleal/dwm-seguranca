using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;
using Seguranca.Models.Entidades;

namespace Seguranca.Models.Repositories
{
    public class GrupoTransacaoViewModel : Repository
    {
        [DisplayName("ID Grupo")]
        [Required(ErrorMessage="Grupo deve ser informado")]
        public int grupoId { get; set; }
        
        [DisplayName("Grupo")]
        public string nome_grupo { get; set; }

        [DisplayName("ID Funcionalidade")]
        [Required(ErrorMessage = "Funcionalidade deve ser informada")]
        public int transacaoId { get; set; }

        [DisplayName("Nome Menu")]
        public string nomeCurto { get; set; }

        [DisplayName("Pai")]
        public string nomeCurtoPai { get; set; }

        [DisplayName("Funcionalidade")]
        public string nome_funcionalidade { get; set; }

        [DisplayName("Referência")]
        public string referencia { get; set; }

        [DisplayName("Sistema")]
        public string nome_sistema { get; set; }

        [DisplayName("Situação")]
        [Required(ErrorMessage = "Situação deve ser informada")]
        public string situacao { get; set; }
    }
}