namespace Ecommerce.Domain
{
    public interface IProductRepository
    {
        void Adicionar(Product produto);
        IEnumerable<Product> ObterTodos();
        Product ObterPorId(Guid id);
        void Atualizar(Product produto);
        void Remover(Guid id);
    }
}
