using BExpress.Infra.Sessao;
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
    [Authorize]
    public class CarrinhoComprasController : PadraoController
    {
        [HttpGet]
        public IActionResult ObterCarrinho()
        {
            try
            {
                return Ok(Sessao.Usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
