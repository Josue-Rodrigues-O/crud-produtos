using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ecommerce.Infrastructure.MySql
{
    public class MySqlProductRepository : IProductRepository
    {
        private readonly MySqlConnection sqlConnection;
        public MySqlProductRepository(IConfiguration configuration)
        {
            var sqlServerConfigs = new MySqlConfig(configuration);
            sqlConnection = MySqlInstance.Create(sqlServerConfigs);
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

        public IEnumerable<Product> GetAll(ProductFilter filter)
        {
            var query = @$"SELECT Id, Codigo, Descricao, Departamento, Preco, Status FROM Products";
            using var command = sqlConnection.CreateCommand();

            if (filter.IncluirItensInativos.GetValueOrDefault())
                query += " WHERE (Status = 1 OR Status = 0)";
            else
                query += " WHERE Status = 1";

            if (!string.IsNullOrWhiteSpace(filter.Codigo))
            {
                query += " AND Codigo = @codigo";
                command.Parameters.AddWithValue("@codigo", filter.Codigo);
            }

            if (!string.IsNullOrWhiteSpace(filter.Descricao))
            {
                query += " AND Descricao LIKE @descricao";
                command.Parameters.AddWithValue("@descricao", $"%{filter.Descricao}%");
            }

            if (!string.IsNullOrWhiteSpace(filter.Departamento))
            {
                query += " AND Departamento = @departamento";
                command.Parameters.AddWithValue("@departamento", filter.Departamento);
            }

            if (filter.PrecoInicial.GetValueOrDefault() > 0)
            {
                query += " AND Preco >= @precoInicial";
                command.Parameters.AddWithValue("@precoInicial", filter.PrecoInicial);
            }

            if (filter.PrecoFinal.GetValueOrDefault() > 0)
            {
                query += " AND Preco <= @precoFinal";
                command.Parameters.AddWithValue("@precoFinal", filter.PrecoFinal);
            }

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

            var codigo = reader.GetString(0);
            var descricao = reader.GetString(1);
            var departamento = reader.GetString(2);
            var preco = reader.GetDecimal(3);
            var status = reader.GetBoolean(4);
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

        ~MySqlProductRepository()
        {
            sqlConnection.Dispose();
        }
    }
}
