using Ecommerce.Domain.Models;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ecommerce.Infrastructure.MySql
{
    public class MySqlDepartmentRepository : IDepartmentRepository
    {
        private readonly MySqlConnection sqlConnection;
        public MySqlDepartmentRepository(IConfiguration configuration)
        {
            var sqlServerConfigs = new MySqlConfig(configuration);
            sqlConnection = MySqlInstance.Create(sqlServerConfigs);
            sqlConnection.Open();
        }

        public IEnumerable<Department> GetAll()
        {
            var query = @"SELECT Id, Codigo, Descricao FROM Departments";
            using var command = sqlConnection.CreateCommand();

            command.CommandText = query;
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var codigo = reader.GetString(1);
                var descricao = reader.GetString(2);
                yield return Department.FromDatabase(id, codigo, descricao);
            }
        }

        public Department? GetByCode(string codigo)
        {
            var query = @"SELECT Id, Codigo, Descricao FROM Departments WHERE Codigo = @codigo";

            using var command = sqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@codigo", codigo);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;

            var id = reader.GetInt32(0);
            var descricao = reader.GetString(2);
            return Department.FromDatabase(id, codigo, descricao);
        }

        ~MySqlDepartmentRepository()
        {
            sqlConnection.Dispose();
        }
    }
}
