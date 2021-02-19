using BExpress.Infra.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(DataContext context) 
            : base(context, context.Pedidos) 
        {
        }
    }
}
