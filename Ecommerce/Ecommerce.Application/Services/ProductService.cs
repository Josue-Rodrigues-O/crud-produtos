using Ecommerce.Application.Dtos;
using Ecommerce.Domain;
using Ecommerce.Domain.Products;

namespace Ecommerce.Application.Services
{
    public class ProductService(IProductRepository repository)
    {
        public Product Create(ProductDto produtoDto)
        {
            var produto = new Product(produtoDto.Codigo, produtoDto.Descricao, produtoDto.Departamento, produtoDto.Preco);
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
            var produto = GetById(id)
                ?? throw new ArgumentException("Não foi possível encontrar o produto informado, verifique se o Id está correto.");

            produto.Update(productDto.Descricao, productDto.Departamento, productDto.Preco);
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
