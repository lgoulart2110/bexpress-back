namespace BExpress.Infra.Entidades
{
    public class Endereco : EntidadePadrao
    {
        public string Descricao { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
