using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Repositorios.Interfaces;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Utilidades;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BExpress.Infra.Servicos
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public RelatorioService(IPedidoRepository pedidoRepository, IPessoaRepository pessoaRepository)
        {
            _pedidoRepository = pedidoRepository;
            _pessoaRepository = pessoaRepository;
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
            _pessoaRepository.Dispose();
        }

        public (List<Pedido>, Pessoa) EmitirRelatorio(RelatorioDto relatorio)
        {
            if (relatorio is null) throw new Exception("Nenhum relatório para imprimir");
            if (!relatorio.DataInicio.HasValue) throw new Exception("Período inicial é obrigatório");
            if (!relatorio.DataFim.HasValue) throw new Exception("Período final é obrigatório");
            if (!relatorio.Tipo.HasValue) throw new Exception("Tipo do relatório é obrigatório");
            if (relatorio.DataFim < relatorio.DataInicio) throw new Exception("Intervalo de datas inválido");

            relatorio.DataFim = relatorio.DataFim.Value.AddDays(1).AddSeconds(-1);

            var dados = new List<Pedido>();

            switch (relatorio.Tipo ?? Enums.eTipoRelatorio.PedidosGeral)
            {
                case Enums.eTipoRelatorio.PedidosGeral: 
                    dados = _pedidoRepository.ObterFiltrado(c => c.DataPedido >= relatorio.DataInicio && c.DataPedido <= relatorio.DataFim).ToList();
                    break;

                case Enums.eTipoRelatorio.PedidosCancelados:
                    dados = _pedidoRepository.ObterFiltrado(c => c.SituacaoPedido == Enums.eSituacaoPedido.Cancelado && c.DataPedido >= relatorio.DataInicio && c.DataPedido <= relatorio.DataFim).ToList();
                    break;

                case Enums.eTipoRelatorio.PedidosFinalizados:
                    dados = _pedidoRepository.ObterFiltrado(c => c.SituacaoPedido == Enums.eSituacaoPedido.Entregue && c.DataPedido >= relatorio.DataInicio && c.DataPedido <= relatorio.DataFim).ToList();
                    break;
            }

            var pessoa = _pessoaRepository.Obter(Sessao.Sessao.Usuario.PessoaId);

            return (dados, pessoa);
        }
    }
}
