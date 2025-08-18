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

        public Product(string codigo, string descricao, string departamento, decimal preco)
        {
            ValidateCodigo(codigo);
            ValidateDescricao(descricao);
            ValidateDepartamento(departamento);
            ValidatePreco(preco);

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

            Departamento = departamento;
            Descricao = descricao;
            Preco = preco;
        }

        public void Delete() => Status = false;

        private static void ValidateCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentException("O campo 'Código' não pode ficar vazio.");

            if (codigo.Length > 255)
                throw new ArgumentException("O código informado é muito extenso, informe um valor menor que 255 caracteres.");
        }

        private static void ValidateDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("O campo 'Descrição' não pode ficar vazio.");
        }

        private static void ValidateDepartamento(string departamento)
        {
            if (string.IsNullOrEmpty(departamento))
                throw new ArgumentException("O campo 'Departamento' não pode ficar vazio.");
        }

        private static void ValidatePreco(decimal preco)
        {
            if (preco <= 0)
                throw new ArgumentException("O campo 'Preço' deve possuir um valor maior que zero.");
        }
    }
}
