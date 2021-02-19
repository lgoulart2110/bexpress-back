using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BExpress.Infra.Entidades
{
    public class CarrinhoCompras : EntidadePadrao
    {
        [NotMapped]
        public List<ItemVenda> Produtos { get; set; } = new List<ItemVenda>();
        public decimal PrecoFrete { get; set; }
        public decimal PrecoFinal { get; set; }
    }
}
