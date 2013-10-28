using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreBorgesLeal.Framework.Models.Entidades
{
    [Table("Sessao")]
    public class Sessao
    {
        [Key]
        [DisplayName("ID Sessão")]
        [Required(ErrorMessage = "ID da sessão deve ser informado")]
        public string sessaoId { get; set; }

        public int sistemaId { get; set; }

        public decimal usuarioId { get; set; }

        public int empresaId { get; set; }

        public DateTime dt_criacao { get; set; }

        public DateTime dt_atualizacao { get; set; }

        public DateTime? dt_desativacao { get; set; }

        public string isOnline { get; set; }

        public virtual Empresa empresa { get; set; }
        public virtual Sistema sistema { get; set; }
        public virtual Usuario usuario { get; set; }
    }

}