using BExpress.Infra.Entidades;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Specification.Consultas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterProduto(int id)
        {
            try
            {
                var produto = _produtoRepository.Obter(id);
                if (produto is null) throw new Exception("Produto não encontrado.");
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("produtos/{categoriaId}")]
        public IActionResult ObterProdutos(int? categoriaId)
        {
            try
            {
                var spec = ProdutoSpecification.Consulte(categoriaId);
                var produtos = _produtoRepository.ObterPorConsulta(spec);
                if (produtos is null || !produtos.Any()) throw new Exception("Nenhum produto encontrado.");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpPost]
        public IActionResult AdicionarProduto([FromBody]Produto produto)
        {
            try
            {
                if (produto is null) throw new Exception("Nenhum produto para adicionar.");
                _produtoRepository.Adicionar(produto);
                _produtoRepository.SalvarAlteracoes();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpPut]
        public IActionResult AlterarProduto([FromBody]Produto produto)
        {
            try
            {
                if (produto is null) throw new Exception("Nenhum produto para alterar.");
                _produtoRepository.Atualizar(produto);
                _produtoRepository.SalvarAlteracoes();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpDelete]
        public IActionResult DeletarProduto([FromQuery]int id)
        {
            try
            {
                var produto = _produtoRepository.Obter(id);
                if (produto is null) throw new Exception("Produto não encontrado.");
                produto.Inativar();
                _produtoRepository.Atualizar(produto);
                _produtoRepository.SalvarAlteracoes();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
