namespace BExpress.Infra.Entidades.Dtos
{
    public class AlterarSenhaDto
    {
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string NovaSenhaRepetir { get; set; }
    }
}
