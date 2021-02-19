using BExpress.Infra.Entidades;
using System;
using System.Collections.Generic;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Produto Obter(int id);
        IEnumerable<Produto> ObterProdutos(int? categoriaId);
        void Adicionar(Produto produto);
        void Alterar(Produto produto);
        void Deletar(int id);
    }
}
