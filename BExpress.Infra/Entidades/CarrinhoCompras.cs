using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BExpress.Infra.Entidades
{
    public class CarrinhoCompras : EntidadePadrao
    {
        public virtual List<ItemVenda> ItemVendas { get; set; } = new List<ItemVenda>();
        public decimal PrecoFrete { get; set; }
        public decimal PrecoFinal { get; set; }

        public void AdicionarProduto(List<ItemVenda> itens)
        {
            ItemVendas.AddRange(itens);
        }

        public void AtualizarValores()
        {
            PrecoFinal = ItemVendas.Sum(c => c.Produto.Preco) + PrecoFrete;
        }
    }
}
