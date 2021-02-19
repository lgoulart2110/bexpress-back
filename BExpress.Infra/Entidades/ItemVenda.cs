using System;

namespace BExpress.Infra.Entidades
{
    public class ItemVenda
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int CarrinhoComprasId { get; set; }
        public CarrinhoCompras CarrinhoCompras { get; set; }
        public int Quantidade { get; set; }
    }
}
