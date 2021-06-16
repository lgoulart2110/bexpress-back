using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BExpress.Infra.Enums
{
    public enum eTipoRelatorio
    {
        [Description("Relatório de pedidos geral")]
        PedidosGeral = 1,

        [Description("Relatório de pedidos finalizados")]
        PedidosFinalizados = 2,

        [Description("Relatório de pedidos cancelados")]
        PedidosCancelados = 3
    }
}
