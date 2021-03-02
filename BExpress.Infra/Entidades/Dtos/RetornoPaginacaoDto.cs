namespace BExpress.Infra.Entidades.Dtos
{
    public class RetornoPaginacaoDto
    {
        public RetornoPaginacaoDto(int totalPaginas, int quantidadeTotal, int pagina, object dados)
        {
            TotalPaginas = totalPaginas;
            QuantidadeTotal = quantidadeTotal;
            Pagina = pagina;
            Dados = dados;
        }

        public int TotalPaginas { get; set; }
        public int QuantidadeTotal { get; set; }
        public int Pagina { get; set; }
        public object Dados { get; set; }
    }
}
