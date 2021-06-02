using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BExpress.Infra.Servicos
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public void Dispose()
        {
            _enderecoRepository.Dispose();
        }

        public void Adicionar(Endereco endereco)
        {
            if (endereco is null) throw new Exception("Nenhum endereco para adicionar");
            endereco.PessoaId = Sessao.Sessao.Usuario.PessoaId;
            endereco.DataCadastro = DateTime.Now;
            _enderecoRepository.Adicionar(endereco);
            _enderecoRepository.SalvarAlteracoes();
        }

        public void Alterar(Endereco endereco)
        {
            if (endereco is null) throw new Exception("Nenhum endereco para alterar");
            endereco.PessoaId = Sessao.Sessao.Usuario.PessoaId;
            _enderecoRepository.Atualizar(endereco);
            _enderecoRepository.SalvarAlteracoes();
        }

        public List<Endereco> Get()
        {
            return _enderecoRepository.ObterFiltrado(c => c.PessoaId == Sessao.Sessao.Usuario.PessoaId).ToList();
        }

        public void Remover(int id)
        {
            var endereco = _enderecoRepository.Obter(id);
            if (endereco is null) throw new Exception("Endereço não encontrado");
            _enderecoRepository.Deletar(endereco);
            _enderecoRepository.SalvarAlteracoes();
        }
    }
}
