using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Enums;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Utilidades;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet]
        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        public IActionResult EmitirRelatorio([FromQuery] eTipoRelatorio tipo, string dataInicio, string dataFim)
        {
            try
            {
                var (dados, pessoa) = _relatorioService.EmitirRelatorio(new RelatorioDto
                {
                    Tipo = tipo,
                    DataFim = DateTime.Parse(dataFim, new CultureInfo("pt-BR")),
                    DataInicio = DateTime.Parse(dataInicio, new CultureInfo("pt-BR"))
                });
                MemoryStream stream = new MemoryStream();
                PdfWriter writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                document.Add(new Paragraph(tipo.GetDescription()));
                document.Add(new Paragraph($"Emissor: {pessoa?.Nome}"));
                document.Add(new Paragraph($"Período inicial: {dataInicio}"));
                document.Add(new Paragraph($"Período final: {dataFim}"));

                Table table = new Table(5).UseAllAvailableWidth();

                table.AddHeaderCell(new Cell().Add(new Paragraph("Cliente")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Data do pedido")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Valor")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Tipo do pagamento")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Situação do pedido")));

                foreach (var item in dados)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.Usuario.Pessoa.Nome)));
                    table.AddCell(new Cell().Add(new Paragraph(item.DataPedido.ToShortDateString())));
                    table.AddCell(new Cell().Add(new Paragraph(Formatadores.FormataDecimalParaReal(item.Valor))));
                    table.AddCell(new Cell().Add(new Paragraph(item.TipoPagamento.GetDescription())));
                    table.AddCell(new Cell().Add(new Paragraph(item.SituacaoPedido.GetDescription())));
                }

                document.Add(table);

                document.Close();

                return File(stream.ToArray(), "application/pdf", "relatorio.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
