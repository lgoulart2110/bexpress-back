using BExpress.Infra.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BExpress.Infra.Entidades
{
    public class Usuario : EntidadePadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Roles { get; set; }
        public eTipoUsuario TipoUsuario { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int CarrinhoComprasId { get; set; }
        public virtual CarrinhoCompras CarrinhoCompras { get; set; }

        [NotMapped]
        public string Token { get; set; }

        public void Excluir()
        {
            Ativo = false;
        }
    }
}
