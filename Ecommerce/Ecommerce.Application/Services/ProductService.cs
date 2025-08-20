using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.Repositories;

namespace Ecommerce.Application.Services
{
    public class ProductService(IProductRepository repository, DepartmentService departmentService)
    {
        public Product Create(ProductDto produtoDto)
        {
            var errors = new Dictionary<string, string[]>();
            var produto = new Product(produtoDto.Codigo, produtoDto.Descricao, produtoDto.Departamento, produtoDto.Preco);

            if (GetAll(new() { Codigo = produto.Codigo, IncluirItensInativos = true }).Any())
                errors.Add("codigo", ["Já existe um produto salvo com o código informado, por favor informe outro código."]);

            if (departmentService.GetByCode(produto.Departamento) is null)
                errors.Add("departamento", ["Não existe departamento com o código informado, por favor informe um departamento válido."]);

            if (errors.Count > 0)
                throw new ValidationException(errors);

            repository.Create(produto);
            return produto;
        }

        public IEnumerable<Product> GetAll(ProductFilter filter)
        {
            return repository.GetAll(filter);
        }

        public Product? GetById(Guid id)
        {
            return repository.GetById(id);
        }

        public void Update(Guid id, ProductDto productDto)
        {
            var errors = new Dictionary<string, string[]>();
            var produto = GetById(id)
                ?? throw new ArgumentException("Não foi possível encontrar o produto informado, verifique se o Id está correto.");

            produto.Update(productDto.Descricao, productDto.Departamento, productDto.Preco);

            if (departmentService.GetByCode(produto.Departamento) is null)
                errors.Add("departamento", ["Não existe departamento com o código informado, por favor informe um departamento válido."]);

            if (errors.Count > 0)
                throw new ValidationException(errors);

            repository.Update(produto);
        }

        public void Delete(Guid id)
        {
            var produto = GetById(id)
                ?? throw new ArgumentException("Não foi possível encontrar o produto informado, verifique se o Id está correto.");

            produto.Delete();
            repository.Delete(produto);
        }
    }
}
