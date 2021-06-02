using BExpress.Infra.Entidades;
using BExpress.Infra.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BExpress.Infra.Base
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Obter(int id);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(T entity);
        void SalvarAlteracoes();
        void AdicionarVarios(IEnumerable<T> entities);
        void DeletarVarios(IEnumerable<T> entities);
        IEnumerable<T> ObterPorConsulta(ISpecification<T> spec);
        IEnumerable<T> ObterFiltrado(Expression<Func<T, bool>> expression);
    }
}
