using Ecommerce.Domain.Products;

namespace Ecommerce.Domain
{
    public interface IProductRepository
    {
        void Create(Product produto);
        IEnumerable<Product> GetAll(ProductFilter filter);
        Product? GetById(Guid id);
        void Update(Product produto);
        void Delete(Product produto);
    }
}
