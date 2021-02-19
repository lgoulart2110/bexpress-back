using BExpress.Infra.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class CarrinhoComprasRepository : Repository<CarrinhoCompras>, ICarrinhoComprasRepository
    {
        public CarrinhoComprasRepository(DataContext context) 
            : base(context, context.CarrinhoCompras) 
        {
        }
    }
}
