using BExpress.Core.Entidades;
using System;

namespace BExpress.Infra.Base
{
    public interface IRepository<T> where T : EntidadePadrao
    {
        T Obter(int id);
        T Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(T entity);
    }
}
