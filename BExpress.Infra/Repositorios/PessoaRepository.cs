using BExpress.Infra.Entidades;
using BExpress.Infra.Base;
using BExpress.Infra.Context;
using BExpress.Infra.Repositorios.Interfaces;

namespace BExpress.Infra.Repositorios
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(DataContext context) 
            : base(context, context.Pessoas) 
        {
        }
    }
}
