﻿using BExpress.Infra.Repositorios;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos;
using BExpress.Infra.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;
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
            services.AddScoped<IItemVendaRepository, ItemVendaRepository>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICarrinhoComprasService, CarrinhoComprasService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IRelatorioService, RelatorioService>();
        }
    }
}
