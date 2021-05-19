using System;

namespace BExpress.Infra.Entidades
{
    public class Produto : EntidadePadrao
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public int QuantidadeEstoque { get; set; }

        public void Inativar()
        {
            Ativo = false;
        }

        public bool DisponivelEmEstoque(int quantidade)
        {
            if (quantidade == 0 || QuantidadeEstoque == 0) return false;
            return QuantidadeEstoque > quantidade;
        }

        public void SubtrairEstoque(int valor)
        {
            QuantidadeEstoque -= valor;
        }
    }
}
