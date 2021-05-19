using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using System;
using System.Collections.Generic;

namespace BExpress.Infra.Servicos
{
    public class CarrinhoComprasService : ICarrinhoComprasService
    {
        private readonly ICarrinhoComprasRepository _carrinhoComprasRepository;
        private readonly IProdutoService _produtoService;

        public CarrinhoComprasService(
            ICarrinhoComprasRepository carrinhoComprasRepository,
            IProdutoService produtoService)
        {
            _carrinhoComprasRepository = carrinhoComprasRepository;
            _produtoService = produtoService;
        }

        public void Dispose()
        {
            _carrinhoComprasRepository.Dispose();
        }

        public void AdicionarProduto(int produtoId, int carrinhoId, int quantidade)
        {
            var carrinho = Obter(carrinhoId);
            var produto = _produtoService.Obter(produtoId);
            List<ItemVenda> itens = new List<ItemVenda>();

            for (var i = 0; i < quantidade; i++)
            {
                var itemVenda = new ItemVenda(produto, carrinho);
                itens.Add(itemVenda);
            }
            
            AtualizarCarrinho(carrinho, produto, itens);
        }
        public void Esvaziar(int id)
        {
            throw new NotImplementedException();
        }

        public CarrinhoCompras Obter(int id)
        {
            var carrinho = _carrinhoComprasRepository.Obter(id);
            if (carrinho is null) throw new Exception("Carrinho de compras não encontrado.");
            return carrinho;
        }

        private void AtualizarCarrinho(CarrinhoCompras carrinho, Produto produto, List<ItemVenda> itens)
        {
            carrinho.AdicionarProduto(itens);
            carrinho.AtualizarValores();
            produto.SubtrairEstoque(itens.Count);
            _produtoService.Alterar(produto);
            _carrinhoComprasRepository.Atualizar(carrinho);
            _carrinhoComprasRepository.SalvarAlteracoes();
        }
    }
}
