using BExpress.Infra.Entidades;
using BExpress.Infra.Context;
using BExpress.Infra.Specification;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace BExpress.Infra.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        DbSet<T> _set;
        DataContext _context;

        public Repository(DataContext context, DbSet<T> set)
        {
            _set = set;
            _context = context;
        }

        public void Adicionar(T entity)
        {
            _set.Add(entity);
        }

        public void AdicionarVarios(IEnumerable<T> entities)
        {
            _set.AddRange(entities);
        }

        public void Atualizar(T entity)
        {
            _set.Update(entity);
        }

        public void Deletar(T entity)
        {
            _set.Remove(entity);
        }

        public void DeletarVarios(IEnumerable<T> entities)
        {
            _set.RemoveRange(entities);
        }

        public T Obter(int id)
        {
            return _set.Find(id);
        }

        public IEnumerable<T> ObterPorConsulta(ISpecification<T> spec)
        {
            return _set.Where(spec.SatisfiedBy()).AsEnumerable();
        }

        public IEnumerable<T> ObterFiltrado(Expression<Func<T, bool>> expression)
        {
            return _set.Where(expression).AsEnumerable();
        }

        public void SalvarAlteracoes()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
