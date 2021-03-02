using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Paginacao;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        public IActionResult AdicionarCategoria([FromBody]Categoria categoria)
        {
            try
            {
                _categoriaService.Adicionar(categoria);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        public IActionResult AlterarCategoria([FromBody]Categoria categoria)
        {
            try
            {
                _categoriaService.Alterar(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [Route("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            try
            {
                _categoriaService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult ObterCategorias()
        {
            try
            {
                var categorias = _categoriaService.ObterCategorias();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("categorias/paginada")]
        public IActionResult ObterCategoriasPaginada(int pagina, int quantidadePagina)
        {
            try
            {
                var categorias = _categoriaService.ObterCategorias();
                var paginacao = Paginar<Categoria>.Pagine(categorias, pagina, quantidadePagina);
                return Ok(
                    new RetornoPaginacaoDto(
                        paginacao.TotalPaginas,
                        paginacao.QuantidadeTotal,
                        paginacao.Pagina,
                        paginacao.Dados
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
