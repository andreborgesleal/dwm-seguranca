using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreBorgesLeal.Framework.Models.Entidades
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DisplayName("Usuário ID")]
        [Required(ErrorMessage = "ID do usuário deve ser informado")]
        public decimal usuarioId { get; set; }

        public int empresaId { get; set; }

        public string login { get; set; }

        public string nome { get; set; }

        public DateTime dt_cadastro { get; set; }

        public string situacao { get; set; }

        public string isAdmin { get; set; }

        public string senha { get; set; }

        public virtual Empresa empresa { get; set; }
    }
}
