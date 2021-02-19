using System.Collections.Generic;

namespace BExpress.Infra.Entidades
{
    public class CarrinhoCompras : EntidadePadrao
    {
        public List<ItemVenda> Produtos { get; set; } = new List<ItemVenda>();
        public decimal PrecoFrete { get; set; }
        public decimal PrecoFinal { get; set; }
    }
}
