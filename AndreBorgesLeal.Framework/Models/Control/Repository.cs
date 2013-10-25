using AndreBorgesLeal.Framework.Models.Contratos;
using AndreBorgesLeal.Framework.Models.Entidades;
using AndreBorgesLeal.Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Web;

namespace AndreBorgesLeal.Framework.Models.Control
{
    public abstract class Repository
    {
        [DisplayName("Session ID")]
        public string sessionId { get; set; }

        [DisplayName("Empresa")]
        public int empresaId { get; set; }

        [DisplayName("Mensagem")]
        public Validate mensagem { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}