using BExpress.Infra.Entidades;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterProduto(int id)
        {
            try
            {
                var produto = _produtoService.Obter(id);
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
                var produtos = _produtoService.ObterProdutos(categoriaId);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpPost]
        public IActionResult AdicionarProduto([FromBody]Produto produto)
        {
            try
            {
                _produtoService.Adicionar(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpPut]
        public IActionResult AlterarProduto([FromBody]Produto produto)
        {
            try
            {
                _produtoService.Alterar(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpDelete]
        public IActionResult DeletarProduto([FromQuery]int id)
        {
            try
            {
                _produtoService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
