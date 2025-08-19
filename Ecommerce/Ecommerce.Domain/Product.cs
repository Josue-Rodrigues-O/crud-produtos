using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain
{
    public class Product
    {
        public Guid Id { get; }
        public string Codigo { get; }
        public string Descricao { get; private set; }
        public string Departamento { get; private set; }
        public decimal Preco { get; private set; }
        public bool Status { get; private set; }

        private readonly Dictionary<string, string[]> _exceptions = [];

        public Product(string codigo, string descricao, string departamento, decimal preco)
        {
            ValidateCodigo(codigo);
            ValidateDescricao(descricao);
            ValidateDepartamento(departamento);
            ValidatePreco(preco);
            CheckForErrors();

            Id = Guid.NewGuid();
            Codigo = codigo;
            Descricao = descricao;
            Departamento = departamento;
            Preco = preco;
            Status = true;
        }

        private Product(Guid id, string codigo, string descricao, string departamento, decimal preco, bool status)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Departamento = departamento;
            Preco = preco;
            Status = status;
        }

        public static Product FromDatabase(Guid id, string codigo, string descricao, string departamento, decimal preco, bool status)
            => new(id, codigo, descricao, departamento, preco, status);

        public void Update(string descricao, string departamento, decimal preco)
        {
            ValidateDescricao(descricao);
            ValidateDepartamento(departamento);
            ValidatePreco(preco);
            CheckForErrors();

            Departamento = departamento;
            Descricao = descricao;
            Preco = preco;
        }

        public void Delete() => Status = false;

        private void ValidateCodigo(string codigo)
        {
            var errors = new List<string>(2);
            if (string.IsNullOrEmpty(codigo))
                errors.Add("O campo 'Código' não pode ficar vazio.");

            if (codigo.Length > 255)
                errors.Add("O código informado é muito extenso, informe um valor menor que 255 caracteres.");

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

        private void ValidateDepartamento(string departamento)
        {
            var errors = new List<string>(1);
            if (string.IsNullOrEmpty(departamento))
                errors.Add("O campo 'Departamento' não pode ficar vazio.");

            if (errors.Count > 0)
                _exceptions["departamento"] = errors.ToArray();
        }

        private void ValidatePreco(decimal preco)
        {
            var errors = new List<string>(1);
            if (preco <= 0)
                errors.Add("O campo 'Preço' deve possuir um valor maior que zero.");

            if (errors.Count > 0)
                _exceptions["preco"] = errors.ToArray();
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
