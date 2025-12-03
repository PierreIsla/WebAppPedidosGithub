namespace WebAppPedidos.Models
{
    public class PedidoPagamento
    {
        public int Id { get; set; }           // PK, Identity no SQL
        public string Titulo { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public decimal ValorComIVA { get; set; }
        public decimal ValorSemIVA { get; set; }
        public string? DocumentoUrl { get; set; } // link/ficheiro no Storage
    }
}