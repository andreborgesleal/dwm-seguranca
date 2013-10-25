﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface ISecurity
    {
        Validate autenticar(string usuario, string senha);
        bool validarSessao(string sessionId);
        void EncerrarSessao(string sessionId);
    }
}
