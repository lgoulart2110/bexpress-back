using BExpress.Infra.Enums;
using System;

namespace BExpress.Infra.Entidades.Dtos
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Roles { get; set; } = "client";
        public eTipoUsuario TipoUsuario { get; set; } = eTipoUsuario.Cliente;
        public bool Ativo { get; set; } = true;
        public DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}
