using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class ItemVendaRepository : Repository<ItemVenda>, IItemVendaRepository
    {
        public ItemVendaRepository(DataContext context)
            : base(context, context.ItemVendas)
        {
        }
    }
}
