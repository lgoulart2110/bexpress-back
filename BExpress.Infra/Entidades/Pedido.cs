using BExpress.Infra.Enums;

namespace BExpress.Infra.Entidades
{
    public class Pedido : EntidadePadrao
    {
        public decimal Valor { get; set; }
        public decimal? Troco { get; set; }
        public eTipoPagamento TipoPagamento { get; set; }
        public eSituacaoPedido SituacaoPedido { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
