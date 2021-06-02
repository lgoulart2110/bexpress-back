using BExpress.Infra.Entidades;
using System;
using System.Collections.Generic;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IEnderecoService : IDisposable
    {
        List<Endereco> Get();
        void Remover(int id);
        void Adicionar(Endereco endereco);
        void Alterar(Endereco endereco);
    }
}
