using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BExpress.Infra.Paginacao
{
    public class Paginar<T>
    {
        public int Pagina { get; private set; }
        public int TotalPaginas { get; private set; }
        public int QuantidadeTotal { get; private set; }
        public object Dados { get; private set; }

        public Paginar(List<T> items, int quantidadeTotal, int pagina, int quantidadePagina)
        {
            Pagina = pagina;
            QuantidadeTotal = quantidadeTotal;
            TotalPaginas = (int)Math.Ceiling(quantidadeTotal / (double)quantidadePagina);
            Dados = items;
        }

        public bool HaPaginaAnterior
        {
            get
            {
                return (Pagina > 1);
            }
        }

        public bool HaPaginaPosterior
        {
            get
            {
                return (Pagina < TotalPaginas);
            }
        }

        public static Paginar<T> Pagine(IEnumerable<T> source, int pagina, int quantidadePagina)
        {
            var quantidadeTotal = source.Count();
            var items = source.Skip((pagina - 1) * quantidadePagina).Take(quantidadePagina).ToList();
            return new Paginar<T>(items, quantidadeTotal, pagina, quantidadePagina);
        }
    }
}
