using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BExpress.Infra.Entidades
{
    public class Pessoa : EntidadePadrao
    {
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual List<Endereco> Enderecos { get; set; }
    }
}
