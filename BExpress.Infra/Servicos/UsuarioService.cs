using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Specification.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BExpress.Infra.Servicos
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }

        public void Adicionar(UsuarioDto usuarioDto)
        {
            if (usuarioDto is null) throw new Exception("Não há usuário para adicionar.");

            var usuarioExistente = _usuarioRepository.ObterFiltrado(c => c.Ativo && (c.Login == usuarioDto.Login || c.Pessoa.CpfCnpj == usuarioDto.Cpf));
            if (usuarioExistente.Any()) throw new Exception("Já existe um usuário cadastrado com esse e-mail ou cpf");

            var pessoa = new Pessoa()
            {
                CpfCnpj = usuarioDto.Cpf,
                DataNascimento = usuarioDto.DataNascimento,
                Nome = usuarioDto.Nome,
                Telefone = usuarioDto.Telefone
            };

            var usuario = new Usuario()
            {
                Ativo = usuarioDto.Ativo,
                CarrinhoCompras = new CarrinhoCompras(),
                DataCadastro = usuarioDto.DataRegistro,
                Login = usuarioDto.Login,
                Pessoa = pessoa,
                Roles = usuarioDto.Roles,
                Senha = usuarioDto.Senha,
                TipoUsuario = usuarioDto.TipoUsuario
            };

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.SalvarAlteracoes();
        }

        public void Deletar(int id)
        {
            var usuario = _usuarioRepository.Obter(id);
            if (usuario is null) throw new Exception("Usuário não encontrado.");
            usuario.Excluir();
            _usuarioRepository.Atualizar(usuario);
            _usuarioRepository.SalvarAlteracoes();
        }

        public Usuario Obter(string login, string senha)
        {
            var usuario = _usuarioRepository.Obter(login, senha);
            if (usuario is null) throw new Exception("Usuário não encontrado.");
            if (!usuario.Ativo) throw new Exception("Usuário desativado.");
            return usuario;
        }

        public void AlterarSenha(int idUsuario, string senhaAtual, string novaSenha)
        {
            if (string.IsNullOrEmpty(senhaAtual) || string.IsNullOrEmpty(novaSenha)) throw new Exception("Digite a senha atual e a nova senha.");
            if (senhaAtual == novaSenha) throw new Exception("Senha atual não pode ser igual a nova senha.");
            var usuario = _usuarioRepository.Obter(idUsuario);
            if (usuario is null) throw new Exception("Usuário não encontrado.");
            usuario.Senha = novaSenha;
            _usuarioRepository.Atualizar(usuario);
            _usuarioRepository.SalvarAlteracoes();
        }

        public IEnumerable<Usuario> ObterUsuarios()
        {
            var usuarios = _usuarioRepository.ObterFiltrado(c => c.Ativo && c.TipoUsuario == Enums.eTipoUsuario.Cliente);
            return usuarios;
        }
    }
}
