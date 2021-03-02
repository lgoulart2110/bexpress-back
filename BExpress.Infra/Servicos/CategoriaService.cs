using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Specification.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BExpress.Infra.Servicos
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaService(
            ICategoriaRepository categoriaRepository,
            IProdutoService produtoService,
            IProdutoRepository produtoRepository)
        {
            _categoriaRepository = categoriaRepository;
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
        }

        public void Dispose()
        {
            _categoriaRepository.Dispose();
            _produtoRepository.Dispose();
            _produtoService.Dispose();
        }

        public void Adicionar(Categoria categoria)
        {
            if (categoria is null) throw new Exception("Nenhuma categoria para adicionar.");

            ValidarCategoria(categoria);
            categoria.Ativa = true;
            categoria.DataCadastro = DateTime.Now;
            _categoriaRepository.Adicionar(categoria);
            _categoriaRepository.SalvarAlteracoes();
        }

        public void Alterar(Categoria categoria)
        {
            if (categoria is null) throw new Exception("Nenhuma categoria para alterar.");

            ValidarCategoria(categoria);

            _categoriaRepository.Atualizar(categoria);
            _categoriaRepository.SalvarAlteracoes();
        }

        public void Deletar(int id)
        {
            var categoria = Obter(id);
            var produtos = _produtoService.ObterProdutos(id);
            foreach (var produto in produtos)
            {
                produto.Inativar();
                _produtoRepository.Atualizar(produto);
            }
            categoria.Inativar();
            _categoriaRepository.Atualizar(categoria);
            _categoriaRepository.SalvarAlteracoes();
        }

        public Categoria Obter(int id)
        {
            var categoria = _categoriaRepository.Obter(id);
            if (categoria is null) throw new Exception("Categoria não encontrada.");
            return categoria;
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            var categorias = _categoriaRepository.ObterFiltrado(c => c.Ativa);
            return categorias;
        }

        private void ValidarCategoria(Categoria categoria)
        {
            var existeCriada = _categoriaRepository.ObterFiltrado(c => c.Ativa && c.Nome == categoria.Nome).Any();
            if (existeCriada) throw new Exception("Já existe uma categoria com esse nome.");
        }
    }
}
