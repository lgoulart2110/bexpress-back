using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Paginacao;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : PadraoController
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        [Route("produtos")]
        public IActionResult ObterProdutos(int pagina, int quantidadePagina, int? categoriaId = null)
        {
            try
            {
                var produtos = _produtoService.ObterProdutos(categoriaId);
                var paginacao = Paginar<Produto>.Pagine(produtos, pagina, quantidadePagina);
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

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(string nome, int categoriaId, string descricao, string preco, int quantidadeEstoque, IFormFile imagem)
        {
            try
            {
                var nomeImagem = await SalvarImagemAsync(imagem);
                var produto = new Produto
                {
                    Ativo = true,
                    CategoriaId = categoriaId,
                    DataCadastro = DateTime.Now,
                    Descricao = descricao,
                    Nome = nome,
                    Imagem = nomeImagem,
                    Preco = Formatadores.FormataRealParaDecimal(preco),
                    QuantidadeEstoque = quantidadeEstoque
                };
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
        public IActionResult AlterarProduto([FromBody]ProdutoDto produto)
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

        private async Task<string> SalvarImagemAsync(IFormFile imagem)
        {
            if (imagem is null) return "padrao.png";
            var path = $"{Directory.GetCurrentDirectory()}/Imagens";
            var extencao = Path.GetExtension(imagem.FileName);

            var extencoesPermitidas = new List<string>
            {
                ".jpg",
                ".jpeg",
                ".png"
            };

            if (!extencoesPermitidas.Contains(extencao))
                throw new Exception("Imagem não permitida.");

            var novoNome = $"{Guid.NewGuid()}{extencao}";
            var fullPath = $"{path}/{novoNome}";

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            return novoNome;
        }
    }
}
