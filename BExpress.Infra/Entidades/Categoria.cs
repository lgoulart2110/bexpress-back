using System;

namespace BExpress.Infra.Entidades
{
    public class Categoria : EntidadePadrao
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
