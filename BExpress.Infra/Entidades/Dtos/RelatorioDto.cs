using BExpress.Infra.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BExpress.Infra.Entidades.Dtos
{
    public class RelatorioDto
    {
        public eTipoRelatorio? Tipo { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
