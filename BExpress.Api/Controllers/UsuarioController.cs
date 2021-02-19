using BExpress.Api.Token;
using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginDto loginDto)
        {
            try
            {
                var usuario = _usuarioService.Obter(loginDto.Login, loginDto.Senha);
                usuario.Token = TokenService.GenerateToken(usuario);
                usuario.Senha = "";
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AdicionarUsuario([FromBody]UsuarioDto usuarioDto)
        {
            try
            {
                _usuarioService.Adicionar(usuarioDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                _usuarioService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult AlterarSenha([FromBody]AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                _usuarioService.AlterarSenha(alterarSenhaDto.Id, alterarSenhaDto.SenhaAtual, alterarSenhaDto.NovaSenha);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
