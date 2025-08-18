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
            ValidarCodigo(codigo);
            ValidarDescricao(descricao);
            ValidarDepartamento(departamento);
            ValidarPreco(preco);

            Id = Guid.NewGuid();
            Codigo = codigo;
            Descricao = descricao;
            Departamento = departamento;
            Preco = preco;
            Status = true;
        }

        public void AlterarDescricao(string descricao)
        {
            ValidarDescricao(descricao);
            Descricao = descricao;
        }
        public void AlterarDepartamento(string departamento)
        {
            ValidarDepartamento(departamento);
            Departamento = departamento;
        }
        public void AlterarPreco(decimal preco)
        {
            ValidarPreco(preco);
            Preco = preco;
        }

        public void Excluir() => Status = false;
        public void Reativar() => Status = true;

        private static void ValidarCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentException("O campo 'Código' não pode ficar vazio.");

            if (codigo.Length > 255)
                throw new ArgumentException("O código informado é muito extenso, informe um valor menor que 255 caracteres.");
        }

        private static void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("O campo 'Descrição' não pode ficar vazio.");
        }

        private static void ValidarDepartamento(string departamento)
        {
            if (string.IsNullOrEmpty(departamento))
                throw new ArgumentException("O campo 'Departamento' não pode ficar vazio.");
        }

        private static void ValidarPreco(decimal preco)
        {
            if (preco <= 0)
                throw new ArgumentException("O campo 'Preço' deve possuir um valor maior que zero.");
        }
    }
}
