using BExpress.Infra.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DataContext context) 
            : base(context, context.Produtos) 
        {
        }
    }
}
