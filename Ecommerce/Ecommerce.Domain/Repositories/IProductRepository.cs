using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Repositories
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
