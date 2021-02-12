using BExpress.Core.Entidades;
using BExpress.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace BExpress.Infra.Base
{
    public class Repository<T> : IRepository<T> where T : EntidadePadrao
    {
        DbSet<T> _set;
        DataContext _context;

        public Repository(DataContext context, DbSet<T> set)
        {
            _set = set;
            _context = context;
        }

        public virtual T Adicionar(T entity)
        {
            _set.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Atualizar(T entity)
        {
            _set.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Deletar(T entity)
        {
            _set.Remove(entity);
            _context.SaveChanges();
        }

        public virtual T Obter(int id)
        {
            return _set.Find(id);
        }

    }
}
