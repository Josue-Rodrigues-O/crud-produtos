namespace Ecommerce.Domain.Filters
{
    public class ProductFilter
    {
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public string? Departamento { get; set; }
        public decimal? PrecoInicial { get; set; }
        public decimal? PrecoFinal { get; set; }
        public bool? IncluirItensInativos { get; set; }
    }
}
