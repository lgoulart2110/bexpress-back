using System;

namespace BExpress.Infra.Entidades
{
    public class ItemVenda
    {
        public ItemVenda() { }

        public ItemVenda(Produto produto, CarrinhoCompras carrinhoCompras)
        {
            ProdutoId = produto.Id;
            CarrinhoComprasId = carrinhoCompras.Id;
        }

        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int CarrinhoComprasId { get; set; }
        public virtual CarrinhoCompras CarrinhoCompras { get; set; }
    }
}
