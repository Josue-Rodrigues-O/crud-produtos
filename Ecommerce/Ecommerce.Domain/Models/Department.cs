using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Models
{
    public class Department
    {
        public int Id { get; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }

        private readonly Dictionary<string, string[]> _exceptions = [];
        public Department(int codigo, string descricao)
        {
            ValidateCodigo(codigo);
            ValidateDescricao(descricao);
            CheckForErrors();

            Codigo = codigo.ToString().PadLeft(3, '0');
            Descricao = descricao;
        }

        private Department(int id, string codigo, string descricao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
        }

        public static Department FromDatabase(int id, string codigo, string descricao)
            => new(id, codigo, descricao);

        private void ValidateCodigo(int codigo)
        {
            var errors = new List<string>(2);
            if (codigo > 999)
                errors.Add("O código informado é muito extenso, informe um valor menor que 1000.");

            if (codigo <= 0)
                errors.Add("O valor do campo 'Código' deve ser maior que 0.");

            if (errors.Count > 0)
                _exceptions["codigo"] = errors.ToArray();
        }

        private void ValidateDescricao(string descricao)
        {
            var errors = new List<string>(1);
            if (string.IsNullOrEmpty(descricao))
                errors.Add("O campo 'Descrição' não pode ficar vazio.");

            if (errors.Count > 0)
                _exceptions["descricao"] = errors.ToArray();
        }

        private void CheckForErrors()
        {
            if (_exceptions.Count > 0)
            {
                throw new ValidationException(_exceptions);
            }
        }
    }
}
