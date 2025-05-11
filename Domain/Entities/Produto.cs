namespace Domain.Entities
{
    public enum CategoriaProduto
    {
        Lanche,
        Acompanhamento,
        Bebida,
        Sobremesa
    }

    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string UrlImagem { get; private set; }
        public decimal Preco { get; private set; }
        public CategoriaProduto Categoria { get; private set; }

        public Produto(string nome, string descricao, string urlImagem, decimal preco, CategoriaProduto categoria)
        {
            Update(nome, descricao, urlImagem, preco, categoria);
        }

        public void Update(string nome, string descricao, string urlImagem, decimal preco, CategoriaProduto categoria)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do produto não pode ser vazio ou nulo.", nameof(nome));

            if (preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.", nameof(preco));

            Nome = nome;
            Descricao = descricao;
            UrlImagem = urlImagem;
            Preco = preco;
            Categoria = categoria;
        }
    }
}