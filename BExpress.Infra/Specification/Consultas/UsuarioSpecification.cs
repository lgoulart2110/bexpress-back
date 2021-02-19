using BExpress.Infra.Entidades;

namespace BExpress.Infra.Specification.Consultas
{
    public static class UsuarioSpecification
    {
        public static Specification<Usuario> Consulte()
        {
            Specification<Usuario> spec = new DirectSpecification<Usuario>(c => true);

            spec &= new DirectSpecification<Usuario>(c => true);

            return spec;
        }
    }
}
