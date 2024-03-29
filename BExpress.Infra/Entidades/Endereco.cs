﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BExpress.Infra.Entidades
{
    public class Endereco : EntidadePadrao
    {
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataCadastro { get; set; }

        [NotMapped]
        public string DescricaoCompleta
        {
            get
            {
                return $"{Logradouro} - {Numero} - {Bairro} - {Cidade} - {Estado} - Cep: {Cep}";
            }
        }
    }
}
