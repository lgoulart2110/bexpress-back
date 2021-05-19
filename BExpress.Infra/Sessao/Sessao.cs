using BExpress.Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BExpress.Infra.Sessao
{
    public class Sessao
    {
        public static Usuario Usuario { get; set; }

        public static void DefinirUsuarioSessao(Usuario usuario) => Usuario = usuario;
    }
}
