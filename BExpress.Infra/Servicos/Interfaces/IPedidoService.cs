using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using System;
using System.Collections.Generic;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IPedidoService : IDisposable
    {
        void RealizarPedido(RealizarPedidoDto realizarPedidoDto);
        List<Pedido> ObterPedidos();
        void CancelarPedido(int pedidoId);
        void AceitarPedido(int pedidoId);
        void EnviarPedido(int pedidoId);
        void FinalizarPedido(int pedidoId);
    }
}
