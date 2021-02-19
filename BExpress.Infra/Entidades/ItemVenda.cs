using System;

namespace BExpress.Infra.Entidades
{
    public class ItemVenda
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
