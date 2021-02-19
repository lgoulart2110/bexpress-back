using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BExpress.Infra.Entidades
{
    public class Pessoa : EntidadePadrao
    {
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        [NotMapped]
        public List<Endereco> Enderecos { get; set; }
    }
}
