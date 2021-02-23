using BExpress.Infra.Entidades;
using System;
using System.Collections.Generic;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface ICategoriaService : IDisposable
    {
        Categoria Obter(int id);
        IEnumerable<Categoria> ObterCategorias();
        void Adicionar(Categoria categoria);
        void Alterar(Categoria categoria);
        void Deletar(int id);
    }
}
