using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using System;

namespace BExpress.Infra.Servicos
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemVendaRepository _itemVendaRepository;
        private readonly ICarrinhoComprasRepository _carrinhoComprasRepository;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IItemVendaRepository itemVendaRepository,
            ICarrinhoComprasRepository carrinhoComprasRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemVendaRepository = itemVendaRepository;
            _carrinhoComprasRepository = carrinhoComprasRepository;
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
            _itemVendaRepository.Dispose();
            _carrinhoComprasRepository.Dispose();
        }

        public void RealizarPedido(RealizarPedidoDto realizarPedidoDto)
        {
            if (realizarPedidoDto is null) throw new Exception("Nenhum pedido à fazer.");
            var carrinho = _carrinhoComprasRepository.Obter(Sessao.Sessao.Usuario.CarrinhoComprasId);
            var descricaoPedido = carrinho.ObterDescricaoPedido();
            var pedido = new Pedido()
            {
                DataPedido = DateTime.Now,
                Descricao = descricaoPedido,
                EnderecoId = realizarPedidoDto.EnderecoId,
                SituacaoPedido = Enums.eSituacaoPedido.Pendente,
                TipoPagamento = (Enums.eTipoPagamento)realizarPedidoDto.TipoPagamento,
                Troco = realizarPedidoDto.Troco,
                Valor = carrinho.PrecoFinal,
                UsuarioId = Sessao.Sessao.Usuario.Id
            };
            _pedidoRepository.Adicionar(pedido);
            _itemVendaRepository.DeletarVarios(carrinho.ItemVendas);
            carrinho.PrecoFinal = decimal.Zero;
            carrinho.PrecoFrete = decimal.Zero;
            _carrinhoComprasRepository.Atualizar(carrinho);
            _pedidoRepository.SalvarAlteracoes();
        }
    }
}
