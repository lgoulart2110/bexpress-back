using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using System;
using System.Collections.Generic;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Usuario Obter(string login, string senha);
        void Adicionar(UsuarioDto usuarioDto);
        void Deletar(int id);
        void AlterarSenha(string senhaAtual, string novaSenha, string novaSenhaRepetir);
        IEnumerable<Usuario> ObterUsuarios();
    }
}
