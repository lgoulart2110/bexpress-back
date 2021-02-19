using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using System;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Usuario Obter(string login, string senha);
        void Adicionar(UsuarioDto usuarioDto);
        void Deletar(int id);
        void AlterarSenha(int idUsuario, string senhaAtual, string novaSenha);
    }
}
