using System;

namespace BExpress.Infra.Entidades
{
    public class ItemVenda
    {
        public ItemVenda() { }

        public ItemVenda(Produto produto, CarrinhoCompras carrinhoCompras, int quantidade)
        {
            Produto = produto;
            CarrinhoComprasId = carrinhoCompras.Id;
            Quantidade = quantidade;
        }

        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int CarrinhoComprasId { get; set; }
        public int Quantidade { get; set; }
    }
}
