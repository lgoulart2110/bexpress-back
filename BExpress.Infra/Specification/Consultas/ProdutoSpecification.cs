using BExpress.Infra.Entidades;

namespace BExpress.Infra.Specification.Consultas
{
    public static class ProdutoSpecification
    {
        public static Specification<Produto> Consulte(int? categoriaId)
        {
            Specification<Produto> spec = new DirectSpecification<Produto>(c => c.Ativo);

            if (categoriaId.HasValue)
                spec = new DirectSpecification<Produto>(c => c.CategoriaId == categoriaId.Value);

            return spec;
        }
    }
}
