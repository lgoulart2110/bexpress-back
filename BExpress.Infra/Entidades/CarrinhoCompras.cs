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

        public int QuantidadeItems => ItemVendas.Count;

        public void AdicionarProduto(ItemVenda item)
        {
            ItemVendas.Add(item);
        }

        public void AtualizarValores()
        {
            PrecoFinal = ItemVendas.Sum(c => c.Produto.Preco * c.Quantidade) + PrecoFrete;
        }

        public string ObterDescricaoPedido()
        {
            if (!ItemVendas.Any()) return null;

            var descricao = string.Empty;

            foreach (var item in ItemVendas)
            {
                descricao += $"<br> # {item.Quantidade} x {item.Produto?.Nome}";
            }

            return descricao;
        }
    }
}
