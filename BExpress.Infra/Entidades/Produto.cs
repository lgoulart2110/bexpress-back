using System;

namespace BExpress.Infra.Entidades
{
    public class Produto : EntidadePadrao
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public bool Ativo { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
