using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Specification.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BExpress.Infra.Servicos
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }

        public void Adicionar(Produto produto)
        {
            if (produto is null) throw new Exception("Nenhum produto para adicionar.");
            _produtoRepository.Adicionar(produto);
            _produtoRepository.SalvarAlteracoes();
        }

        public void Alterar(Produto produto)
        {
            if (produto is null) throw new Exception("Nenhum produto para alterar.");
            _produtoRepository.Atualizar(produto);
            _produtoRepository.SalvarAlteracoes();
        }

        public void Deletar(int id)
        {
            var produto = _produtoRepository.Obter(id);
            if (produto is null) throw new Exception("Produto não encontrado.");
            produto.Inativar();
            _produtoRepository.Atualizar(produto);
            _produtoRepository.SalvarAlteracoes();
        }

        public Produto Obter(int id)
        {
            var produto = _produtoRepository.Obter(id);
            if (produto is null) throw new Exception("Produto não encontrado.");
            return produto;
        }

        public IEnumerable<Produto> ObterProdutos(int? categoriaId)
        {
            var spec = ProdutoSpecification.Consulte(categoriaId);
            var produtos = _produtoRepository.ObterPorConsulta(spec);
            return produtos;
        }
    }
}
