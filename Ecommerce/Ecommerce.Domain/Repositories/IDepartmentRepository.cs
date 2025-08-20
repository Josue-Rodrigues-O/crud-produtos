using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department? GetByCode(string code);
    }
}
