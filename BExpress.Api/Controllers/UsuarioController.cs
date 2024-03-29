﻿using BExpress.Api.Token;
using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Paginacao;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Sessao;
using BExpress.Infra.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : PadraoController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(
            IUsuarioService usuarioService,
            ICategoriaService categoriaService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("jwt")]
        [Authorize]
        public IActionResult VerificarToken(Usuario usuario)
        {
            Sessao.DefinirUsuarioSessao(usuario);
            return Ok();
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
                Sessao.DefinirUsuarioSessao(usuario);
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
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
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

        [HttpGet]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [Route("usuarios/paginado")]
        public IActionResult ObterUsuarios(int pagina, int quantidadePagina)
        {
            try
            {
                var usuarios = _usuarioService.ObterUsuarios();
                var paginacao = Paginar<Usuario>.Pagine(usuarios, pagina, quantidadePagina);
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
        [Authorize]
        public IActionResult AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                _usuarioService.AlterarSenha(alterarSenhaDto.SenhaAtual, alterarSenhaDto.NovaSenha, alterarSenhaDto.NovaSenhaRepetir);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
