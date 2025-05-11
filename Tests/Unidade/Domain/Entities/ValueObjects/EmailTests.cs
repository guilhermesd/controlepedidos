using Domain.Entities.ValueObjects;

namespace Tests.Unidade.Domain.Entities.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("usuario@dominio.com")]
        [InlineData("user.name+alias@domain.co.uk")]
        [InlineData("user_name@sub.domain.com")]
        public void Deve_Criar_Email_Valido(string endereco)
        {
            var email = new Email(endereco);

            Assert.Equal(endereco.Trim(), email.Endereco);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Nao_Deve_Aceitar_Email_Nulo_Ou_Vazio(string enderecoInvalido)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Email(enderecoInvalido));
            Assert.Equal("E-mail não pode ser nulo ou vazio. (Parameter 'endereco')", ex.Message);
        }

        [Theory]
        [InlineData("sem-arroba.com")]
        [InlineData("invalido@")]
        [InlineData("@dominio.com")]
        [InlineData("usuario@@dominio.com")]
        [InlineData("usuario@dominio")]
        public void Nao_Deve_Aceitar_Email_Com_Formato_Invalido(string enderecoInvalido)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Email(enderecoInvalido));
            Assert.Equal("Formato de e-mail inválido. (Parameter 'endereco')", ex.Message);
        }

        [Fact]
        public void Nao_Deve_Aceitar_Email_Muito_Grande()
        {
            // Gera um e-mail com 246 caracteres + "@dominio.com" (13) = 259
            var endereco = new string('a', 246) + "@dominio.com";

            var ex = Assert.Throws<ArgumentException>(() => new Email(endereco));
            Assert.Equal("E-mail excede o tamanho máximo de 254 caracteres. (Parameter 'endereco')", ex.Message);
        }

        [Fact]
        public void Emails_Com_Mesmo_Endereco_Devem_Ser_Iguais()
        {
            var email1 = new Email("usuario@dominio.com");
            var email2 = new Email("USUARIO@DOMINIO.COM");

            Assert.Equal(email1, email2);
            Assert.True(email1.Equals(email2));
            Assert.Equal(email1.GetHashCode(), email2.GetHashCode());
        }
    }
}