using BExpress.Infra.Enums;
using System;

namespace BExpress.Infra.Entidades
{
    public class Usuario : EntidadePadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Roles { get; set; }
        public eTipoUsuario TipoUsuario { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataRegistro { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public int CarrinhoComprasId { get; set; }
        public CarrinhoCompras CarrinhoCompras { get; set; }
        public string Imagem { get; set; }
    }
}
