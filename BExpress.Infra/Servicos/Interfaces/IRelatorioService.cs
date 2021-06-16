using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BExpress.Infra.Servicos.Interfaces
{
    public interface IRelatorioService : IDisposable
    {
        (List<Pedido>, Pessoa) EmitirRelatorio(RelatorioDto relatorio);
    }
}
