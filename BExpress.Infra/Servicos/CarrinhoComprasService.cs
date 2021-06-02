using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using System;
using System.Linq;

namespace BExpress.Infra.Servicos
{
    public class CarrinhoComprasService : ICarrinhoComprasService
    {
        private readonly ICarrinhoComprasRepository _carrinhoComprasRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IItemVendaRepository _itemVendaRepository;

        public CarrinhoComprasService(
            ICarrinhoComprasRepository carrinhoComprasRepository,
            IProdutoRepository produtoRepository,
            IItemVendaRepository itemVendaRepository)
        {
            _carrinhoComprasRepository = carrinhoComprasRepository;
            _produtoRepository = produtoRepository;
            _itemVendaRepository = itemVendaRepository;
        }

        public void Dispose()
        {
            _carrinhoComprasRepository.Dispose();
            _produtoRepository.Dispose();
            _itemVendaRepository.Dispose();
        }

        public void AdicionarProduto(int produtoId, int quantidade)
        {
            var carrinho = Obter(Sessao.Sessao.Usuario.CarrinhoComprasId);
            var existeProduto = carrinho.ItemVendas.Any(c => c.ProdutoId == produtoId);
            if (existeProduto) throw new Exception("Já existe esse produto em seu carrinho, caso queira alterar a quantidade, remova o produto do carrinho e adicione novamente.");
            var produto = _produtoRepository.Obter(produtoId);
            var itemVenda = new ItemVenda(produto, carrinho, quantidade);            
            AtualizarCarrinho(carrinho, produto, itemVenda);
        }

        public CarrinhoCompras Obter(int id)
        {
            var carrinho = _carrinhoComprasRepository.Obter(id);
            if (carrinho is null) throw new Exception("Carrinho de compras não encontrado.");
            return carrinho;
        }

        public void RemoverProduto(int produtoId)
        {
            var produto = _produtoRepository.Obter(produtoId);
            var itemVenda = _itemVendaRepository.ObterFiltrado(c => c.ProdutoId == produtoId && c.CarrinhoComprasId == Sessao.Sessao.Usuario.CarrinhoComprasId).FirstOrDefault();
            produto.RetornarEstoque(itemVenda.Quantidade);
            _produtoRepository.Atualizar(produto);
            _itemVendaRepository.Deletar(itemVenda);
            _carrinhoComprasRepository.SalvarAlteracoes();
        }

        public void CalcularFrete()
        {
            var carrinho = Obter(Sessao.Sessao.Usuario.CarrinhoComprasId);
            carrinho.PrecoFrete = 5;
            carrinho.AtualizarValores();
            _carrinhoComprasRepository.Atualizar(carrinho);
            _carrinhoComprasRepository.SalvarAlteracoes();
        }

        private void AtualizarCarrinho(CarrinhoCompras carrinho, Produto produto, ItemVenda item)
        {
            carrinho.AdicionarProduto(item);
            carrinho.AtualizarValores();
            produto.SubtrairEstoque(item.Quantidade);
            _produtoRepository.Atualizar(produto);
            _carrinhoComprasRepository.Atualizar(carrinho);
            _carrinhoComprasRepository.SalvarAlteracoes();
        }
    }
}
