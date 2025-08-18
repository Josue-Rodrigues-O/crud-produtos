namespace Ecommerce.Domain
{
    public interface IProductRepository
    {
        void Create(Product produto);
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);
        void Update(Product produto);
        void Delete(Product produto);
    }
}
