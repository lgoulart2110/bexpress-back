using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BExpress.Infra.Entidades.Dtos
{
    public class RealizarPedidoDto
    {
        public string Troco { get; set; }
        public int TipoPagamento { get; set; }
        public int EnderecoId { get; set; }
    }
}
