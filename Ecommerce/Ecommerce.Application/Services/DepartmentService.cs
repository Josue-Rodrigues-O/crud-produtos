using Ecommerce.Domain.Models;
using Ecommerce.Domain.Repositories;

namespace Ecommerce.Application.Services
{
    public class DepartmentService(IDepartmentRepository repository)
    {
        public IEnumerable<Department> GetAll()
        {
            return repository.GetAll();
        }

        public Department? GetByCode(string codigo)
        {
            return repository.GetByCode(codigo);
        }
    }
}
