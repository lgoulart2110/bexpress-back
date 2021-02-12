using BExpress.Core.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private DataContext _context;

        public UsuarioRepository(DataContext context) 
            : base(context, context.Usuarios) 
        {
            _context = context;
        }

    }
}
