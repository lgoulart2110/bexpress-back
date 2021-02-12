using System.Collections.Generic;

namespace BExpress.Core.Entidades
{
    public class Pessoa : EntidadePadrao
    {
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}
