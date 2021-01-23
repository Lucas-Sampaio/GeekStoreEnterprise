namespace GeekStore.BFF.Compras.Models
{
    public class VoucherDTO
    {
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int TipoDesconto { get; set; }

    }
    public enum TipoDesconto
    {
        Porcentagem = 0,
        Valor = 1
    }
}
