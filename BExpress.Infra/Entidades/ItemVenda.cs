using System;

namespace BExpress.Infra.Entidades
{
    public class ItemVenda
    {
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int CarrinhoComprasId { get; set; }
        public virtual CarrinhoCompras CarrinhoCompras { get; set; }
        public int Quantidade { get; set; }
    }
}
