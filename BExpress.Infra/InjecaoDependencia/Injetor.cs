using BExpress.Infra.Repositorios;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos;
using BExpress.Infra.Servicos.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BExpress.Infra.InjecaoDependencia
{
    public static  class Injetor
    {
        public static void Injetar(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICarrinhoComprasRepository, CarrinhoComprasRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
        }
    }
}
