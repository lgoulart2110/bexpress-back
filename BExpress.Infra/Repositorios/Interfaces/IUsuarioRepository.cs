using BExpress.Infra.Entidades;
using BExpress.Infra.Base;

namespace BExpress.Infra.Repositorios.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Obter(string login, string senha);
    }
}
