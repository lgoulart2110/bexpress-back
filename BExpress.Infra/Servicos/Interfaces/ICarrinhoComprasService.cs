using BExpress.Infra.Entidades;
using System;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface ICarrinhoComprasService : IDisposable
    {
        CarrinhoCompras Obter(int id);
        void AdicionarProduto(int produtoId, int quantidade);
        void RemoverProduto(int produtoId);
        void CalcularFrete();
    }
}
