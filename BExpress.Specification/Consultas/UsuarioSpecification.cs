using BExpress.Infra.Entidades;
using BExpress.Specification.Specifications;

namespace BExpress.Specification.Consultas
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
