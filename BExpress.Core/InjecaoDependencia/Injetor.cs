using Microsoft.Extensions.DependencyInjection;
using BExpress

namespace BExpress.Core.InjecaoDependencia
{
    public static  class Injetor
    {
        public static void Resolve(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IInformacoesRepository, InformacoesRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITenantService, TenantService>();
        }
    }
}
