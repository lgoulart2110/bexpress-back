using BExpress.Infra.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;
using System.Linq;

namespace BExpress.Infra.Repositorios
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext context) 
            : base(context, context.Usuarios) 
        {
        }

        public Usuario Obter(string login, string senha)
        {
            return base.ObterFiltrado(c => c.Login == login && c.Senha == senha && c.Ativo)
                .FirstOrDefault();
        }
    }
}
