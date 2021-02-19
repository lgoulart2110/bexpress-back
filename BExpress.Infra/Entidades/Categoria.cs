using System;

namespace BExpress.Infra.Entidades
{
    public class Categoria : EntidadePadrao
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
