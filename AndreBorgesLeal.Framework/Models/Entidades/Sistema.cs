using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreBorgesLeal.Framework.Models.Entidades
{
    [Table("Sistema")]
    public class Sistema
    {
        [Key]
        [DisplayName("Sistema ID")]
        [Required(ErrorMessage = "ID do sistema deve ser informado")]
        public decimal sistemaId { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

    }
}
