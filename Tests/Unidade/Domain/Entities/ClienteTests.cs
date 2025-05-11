using Domain.Entities;

namespace Tests.Unidade.Domain.Entities
{
    public class ClienteTests
    {
        [Fact]
        public void Deve_Criar_Cliente_Valido()
        {
            // Arrange
            var nome = "João Silva";
            var cpf = "31960926829"; // Assumindo que é um CPF válido para o VO
            var email = "joao@exemplo.com";

            // Act
            var cliente = new Cliente(nome, cpf, email);

            // Assert
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(cpf, cliente.Cpf.Numero); // Supondo que o VO Cpf tenha uma propriedade Numero
            Assert.Equal(email, cliente.Email.Endereco);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Nao_Deve_Criar_Cliente_Com_Nome_Invalido(string nomeInvalido)
        {
            var cpf = "31960926829";
            var email = "cliente@teste.com";

            var ex = Assert.Throws<ArgumentException>(() => new Cliente(nomeInvalido, cpf, email));
            Assert.Equal("O nome do cliente não pode ser vazio ou nulo. (Parameter 'nome')", ex.Message);
        }

        [Fact]
        public void Nao_Deve_Criar_Cliente_Com_Cpf_Invalido()
        {
            var nome = "Maria";
            var cpf = "111"; // inválido, VO deve lançar
            var email = "maria@teste.com";

            Assert.Throws<ArgumentException>(() => new Cliente(nome, cpf, email));
        }

        [Fact]
        public void Nao_Deve_Criar_Cliente_Com_Email_Invalido()
        {
            var nome = "Carlos";
            var cpf = "31960926829";
            var email = "email-invalido";

            Assert.Throws<ArgumentException>(() => new Cliente(nome, cpf, email));
        }
    }
}
