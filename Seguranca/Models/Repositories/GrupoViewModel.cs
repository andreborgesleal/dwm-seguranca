using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;
using Seguranca.Models.Entidades;


namespace Seguranca.Models.Repositories
{
    public class GrupoViewModel : Repository
    {
        [DisplayName("Grupo Id")]
        public int grupoId { get; set; }

        [DisplayName("Sistema Id")]
        public int sistemaId { get; set; }

        [DisplayName("Empresa Id")]
        public int empresaId { get; set; }

        [DisplayName("Descricao Id")]
        [Required(ErrorMessage="Por favor, preencha o campo descrição")]
        [StringLength(40, ErrorMessage="A quantidade máxima de caracteres permitidos é 40")]
        public string descricao { get; set; }

        [DisplayName("Situação")]
        [Required(ErrorMessage = "Por favor, preencha o campo situação")]
        public string situacao { get; set; }

        public string nome_sistema { get; set; }
    }
}