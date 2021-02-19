namespace BExpress.Infra.Entidades.Dtos
{
    public class AlterarSenhaDto
    {
        public int Id { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
    }
}
