using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Specification.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Pedido> ObterPedidos()
        {
            var spec = PedidoSpecification.Consulte();
            return _pedidoRepository.ObterPorConsulta(spec).OrderByDescending(c => c.Id).ToList();
        }

        public void CancelarPedido(int pedidoId)
        {
            var pedido = Obter(pedidoId);
            if (pedido.SituacaoPedido != Enums.eSituacaoPedido.Pendente && pedido.SituacaoPedido != Enums.eSituacaoPedido.Aceito) throw new Exception("O pedido não pode mais ser cancelado.");
            pedido.SituacaoPedido = Enums.eSituacaoPedido.Cancelado;
            _pedidoRepository.Atualizar(pedido);
            _pedidoRepository.SalvarAlteracoes();
        }

        public void AceitarPedido(int pedidoId)
        {
            var pedido = Obter(pedidoId);
            if (pedido.SituacaoPedido != Enums.eSituacaoPedido.Pendente) throw new Exception("O pedido não pode mais ser aceito.");
            pedido.SituacaoPedido = Enums.eSituacaoPedido.Aceito;
            _pedidoRepository.Atualizar(pedido);
            _pedidoRepository.SalvarAlteracoes();
        }

        public void EnviarPedido(int pedidoId)
        {
            var pedido = Obter(pedidoId);
            if (pedido.SituacaoPedido != Enums.eSituacaoPedido.Aceito) throw new Exception("O pedido não pode mais ser enviado.");
            pedido.SituacaoPedido = Enums.eSituacaoPedido.AguardandoEntrega;
            _pedidoRepository.Atualizar(pedido);
            _pedidoRepository.SalvarAlteracoes();
        }

        public void FinalizarPedido(int pedidoId)
        {
            var pedido = Obter(pedidoId);
            if (pedido.SituacaoPedido != Enums.eSituacaoPedido.AguardandoEntrega) throw new Exception("O pedido não pode mais ser finalizado.");
            pedido.SituacaoPedido = Enums.eSituacaoPedido.Entregue;
            _pedidoRepository.Atualizar(pedido);
            _pedidoRepository.SalvarAlteracoes();
        }

        private Pedido Obter(int id)
        {
            var pedido = _pedidoRepository.Obter(id);
            if (pedido is null) throw new Exception("Pedido não encontrado.");
            return pedido;
        }
    }
}
