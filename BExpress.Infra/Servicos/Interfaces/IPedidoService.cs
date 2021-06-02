using BExpress.Infra.Entidades.Dtos;
using System;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IPedidoService : IDisposable
    {
        void RealizarPedido(RealizarPedidoDto realizarPedidoDto);
    }
}
