using BExpress.Infra.Enums;
using BExpress.Infra.Utilidades;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BExpress.Infra.Entidades
{
    public class Pedido : EntidadePadrao
    {
        public decimal Valor { get; set; }
        public string Troco { get; set; }
        public eTipoPagamento TipoPagamento { get; set; }
        public eSituacaoPedido SituacaoPedido { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public DateTime DataPedido { get; set; }
        public string Descricao { get; set; }

        [NotMapped]
        public string DescricaoCompleta 
        { get
            {
                var valorFormatado = $"R$ {Valor}".Replace(".", ",");
                var endereco = Endereco?.DescricaoCompleta;
                var tipoPagamento = TipoPagamento.GetDescription();
                var descPag = TipoPagamento == eTipoPagamento.Cartao ? tipoPagamento : $"{tipoPagamento} {Troco}";
                var data = DataPedido.ToShortDateString();
                return $"{Descricao} <br> " +
                    $"Total: {valorFormatado} <br> " +
                    $"Endereço de entrega: {endereco} <br> " +
                    $"Pagamento: {descPag} <br> " +
                    $"Data do pedido: {data}";
            } 
        }
    }
}
