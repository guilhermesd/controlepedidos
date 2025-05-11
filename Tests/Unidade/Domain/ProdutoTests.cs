using Domain.Entities;

namespace Tests.Unidade.Domain
{
    public class ProdutoTests
    {
        [Fact]
        public void Deve_Criar_Produto_Valido()
        {
            // Arrange
            string nome = "X-Burguer";
            string descricao = "Lanche com hambúrguer, queijo e salada";
            string urlImagem = "https://site.com/xburguer.jpg";
            decimal preco = 25.50m;
            CategoriaProduto categoria = CategoriaProduto.Lanche;

            // Act
            var produto = new Produto(nome, descricao, urlImagem, preco, categoria);

            // Assert
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(descricao, produto.Descricao);
            Assert.Equal(urlImagem, produto.UrlImagem);
            Assert.Equal(preco, produto.Preco);
            Assert.Equal(categoria, produto.Categoria);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Nao_Deve_Criar_Produto_Com_Nome_Invalido(string nomeInvalido)
        {
            // Arrange
            string descricao = "Produto sem nome válido";
            string urlImagem = "https://site.com/produto.jpg";
            decimal preco = 10.0m;
            CategoriaProduto categoria = CategoriaProduto.Bebida;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Produto(nomeInvalido, descricao, urlImagem, preco, categoria));

            Assert.Equal("O nome do produto não pode ser vazio ou nulo. (Parameter 'nome')", ex.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Nao_Deve_Criar_Produto_Com_Preco_Invalido(decimal precoInvalido)
        {
            // Arrange
            string nome = "Água Mineral";
            string descricao = "Bebida refrescante";
            string urlImagem = "https://site.com/agua.jpg";
            CategoriaProduto categoria = CategoriaProduto.Bebida;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Produto(nome, descricao, urlImagem, precoInvalido, categoria));

            Assert.Equal("O preço do produto deve ser maior que zero. (Parameter 'preco')", ex.Message);
        }
    }
}