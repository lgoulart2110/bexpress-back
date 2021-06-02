﻿using BExpress.Infra.Enums;
using System;

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
    }
}
