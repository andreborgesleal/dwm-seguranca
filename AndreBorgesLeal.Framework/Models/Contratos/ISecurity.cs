using AndreBorgesLeal.Framework.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface ISecurity
    {
        Validate Autenticar(string usuario, string senha, int sistemaId);
        Sessao CriarSessao(string usuario, int sistemaId);
        bool ValidarSessao(string sessionId);
        void EncerrarSessao(string sessionId);
    }
}
