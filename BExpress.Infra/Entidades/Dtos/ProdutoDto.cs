using System;

namespace BExpress.Infra.Entidades.Dtos
{
    public class ProdutoDto : EntidadePadrao
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Preco { get; set; }
        public int CategoriaId { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
