using Ecommerce.Domain;
using Microsoft.Data.SqlClient;

namespace Ecommerce.Infrastructure.SqlServer
{
    public sealed class ProductRepository : IDisposable, IProductRepository
    {
        private readonly SqlConnection sqlConnection;
        public ProductRepository()
        {
            sqlConnection = SqlInstance.Create();
            sqlConnection.Open();
        }

        public void Create(Product produto)
        {
            var query = @"INSERT INTO Products (Id, Codigo, Descricao, Departamento, Preco, Status)
                        VALUES (@id, @codigo, @descricao, @departamento, @preco, @status)";

            var command = sqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", produto.Id);
            command.Parameters.AddWithValue("@codigo", produto.Codigo);
            command.Parameters.AddWithValue("@descricao", produto.Descricao);
            command.Parameters.AddWithValue("@departamento", produto.Departamento);
            command.Parameters.AddWithValue("@preco", produto.Preco);
            command.Parameters.AddWithValue("@status", produto.Status);
            command.ExecuteReader();
        }

        public IEnumerable<Product> GetAll()
        {
            var query = @"SELECT Id, Codigo, Descricao, Departamento, Preco, Status FROM Products";

            using var command = sqlConnection.CreateCommand();
            command.CommandText = query;
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = reader.GetGuid(0);
                var codigo = reader.GetString(1);
                var descricao = reader.GetString(2);
                var departamento = reader.GetString(3);
                var preco = reader.GetDecimal(4);
                var status = reader.GetBoolean(5);
                yield return Product.FromDatabase(id, codigo, descricao, departamento, preco, status);
            }
        }

        public Product? GetById(Guid id)
        {
            var query = @"SELECT Codigo, Descricao, Departamento, Preco, Status FROM Products WHERE Id = @id";

            using var command = sqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (!reader.Read()) return null;

            var codigo = reader.GetString(1);
            var descricao = reader.GetString(2);
            var departamento = reader.GetString(3);
            var preco = reader.GetDecimal(4);
            var status = reader.GetBoolean(5);
            return Product.FromDatabase(id, codigo, descricao, departamento, preco, status);
        }

        public void Update(Product produto)
        {
            var query = @"UPDATE Products set 
                        Descricao = @descricao, Departamento = @departamento, Preco = @preco
                        WHERE Id = @id";

            using var command = sqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", produto.Id);
            command.Parameters.AddWithValue("@descricao", produto.Descricao);
            command.Parameters.AddWithValue("@departamento", produto.Departamento);
            command.Parameters.AddWithValue("@preco", produto.Preco);
            command.ExecuteNonQuery();
        }

        public void Delete(Product produto)
        {
            var query = @"UPDATE Products set Status = @status WHERE Id = @id";

            using var command = sqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", produto.Id);
            command.Parameters.AddWithValue("@status", produto.Status);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            sqlConnection.Dispose();
        }
    }
}
