using BExpress.Infra.Entidades;

namespace BExpress.Infra.Specification.Consultas
{
    public static class PedidoSpecification
    {
        public static Specification<Pedido> Consulte()
        {
            Specification<Pedido> spec = new DirectSpecification<Pedido>(c => true);

            if (Sessao.Sessao.Usuario.TipoUsuario == Enums.eTipoUsuario.Cliente)
                spec &= new DirectSpecification<Pedido>(c => c.UsuarioId == Sessao.Sessao.Usuario.Id);

            return spec;
        }
    }
}
