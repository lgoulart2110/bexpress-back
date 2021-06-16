namespace BExpress.Infra.Utilidades
{
    public static class Formatadores
    {
        public static decimal FormataRealParaDecimal(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return decimal.Zero;

            var newString = valor.Replace("R$", "").Replace(" ", "").Replace(".", "");
            return decimal.Parse(newString);
        }

        public static string FormataDecimalParaReal(decimal valor)
        {
            return $"R$ {valor}";
        }
    }
}
