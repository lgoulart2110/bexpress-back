using BExpress.Core.Enums;
using System;

namespace BExpress.Core.Entidades
{
    public class Usuario : EntidadePadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public eTipoUsuario TipoUsuario { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
