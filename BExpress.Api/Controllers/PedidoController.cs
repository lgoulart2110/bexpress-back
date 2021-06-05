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
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public IActionResult AdicionarPedido([FromBody] RealizarPedidoDto pedido)
        {
            try
            {
                _pedidoService.RealizarPedido(pedido);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterPedidos(int pagina, int quantidade)
        {
            try
            {
                var pedidos = _pedidoService.ObterPedidos();
                var paginacao = Paginar<Pedido>.Pagine(pedidos, pagina, quantidade);
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

        [HttpPut]
        [Route("cancelar/{pedidoId}")]
        public IActionResult CancelarPedido(int pedidoId)
        {
            try
            {
                _pedidoService.CancelarPedido(pedidoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("aceitar/{pedidoId}")]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        public IActionResult AceitarPedido(int pedidoId)
        {
            try
            {
                _pedidoService.AceitarPedido(pedidoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("enviar/{pedidoId}")]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        public IActionResult EnviarPedido(int pedidoId)
        {
            try
            {
                _pedidoService.EnviarPedido(pedidoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("finalizar/{pedidoId}")]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        public IActionResult FinalizarPedido(int pedidoId)
        {
            try
            {
                _pedidoService.FinalizarPedido(pedidoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
