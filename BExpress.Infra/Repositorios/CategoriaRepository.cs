using BExpress.Infra.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DataContext context) 
            : base(context, context.Categorias) 
        {
        }
    }
}
