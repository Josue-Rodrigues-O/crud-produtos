namespace Ecommerce.Application.Dtos
{
    public class ProductDto
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string Departamento { get; private set; }
        public decimal Preco { get; private set; }
        public bool Status { get; private set; }
    }
}
