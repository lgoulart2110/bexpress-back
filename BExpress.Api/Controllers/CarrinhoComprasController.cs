using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Sessao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CarrinhoComprasController : PadraoController
    {
        private readonly ICarrinhoComprasService _carrinhoComprasService;

        public CarrinhoComprasController(ICarrinhoComprasService carrinhoComprasService)
        {
            _carrinhoComprasService = carrinhoComprasService;
        }

        [HttpGet]
        public IActionResult ObterCarrinho()
        {
            try
            {
                var carrinho = _carrinhoComprasService.Obter(Sessao.Usuario.CarrinhoComprasId);
                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("adicionar")]
        public IActionResult AdicionarProduto([FromBody] AdicionarProdutoDto produto)
        {
            try
            {
                _carrinhoComprasService.AdicionarProduto(produto.ProdutoId, produto.Quantidade);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{produtoId}")]
        public IActionResult RemoverProduto(int produtoId)
        {
            try
            {
                _carrinhoComprasService.RemoverProduto(produtoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("frete")]
        public IActionResult CalcularFrete()
        {
            try
            {
                _carrinhoComprasService.CalcularFrete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
