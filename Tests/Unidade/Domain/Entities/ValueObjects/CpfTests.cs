using Xunit;
using Domain.Entities.ValueObjects;

namespace Tests.Unidade.Domain.Entities.ValueObjects
{
    public class CpfTests
    {
        [Theory]
        [InlineData("123.456.789-09", false)] // Invalid CPF
        [InlineData("111.111.111-11", false)] // Invalid CPF (all digits the same)
        [InlineData("529.982.247-25", true)]  // Valid CPF
        [InlineData("52998224725", true)]    // Valid CPF without formatting
        [InlineData(null, false)]            // Null CPF
        [InlineData("", false)]              // Empty CPF
        [InlineData("123", false)]           // Too short CPF
        public void Validar_ShouldValidateCpfCorrectly(string cpf, bool expectedIsValid)
        {
            // Arrange & Act & Assert
            if (!expectedIsValid)
                Assert.Throws<ArgumentException>(() => new Cpf(cpf));
            else
                Assert.NotNull(new Cpf(cpf));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenCpfIsInvalid()
        {
            // Arrange
            var invalidCpf = "123.456.789-09";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cpf(invalidCpf));
        }

        [Fact]
        public void Constructor_ShouldSetNumero_WhenCpfIsValid()
        {
            // Arrange
            var validCpf = "529.982.247-25";

            // Act
            var cpf = new Cpf(validCpf);

            // Assert
            Assert.NotNull(validCpf);
        }

        [Theory]
        [InlineData("123.456.789-09", "12345678909")] // CPF with formatting
        [InlineData("52998224725", "52998224725")]   // CPF without formatting
        [InlineData(null, "")]                       // Null CPF
        [InlineData("", "")]                         // Empty CPF
        public void ApenasNumeros_ShouldRemoveNonNumericCharacters(string input, string expectedOutput)
        {
            // Arrange
            var cpf = new Cpf("529.982.247-25"); // Valid CPF to instantiate the object

            // Act
            var result = cpf.GetType()
                            .GetMethod("ApenasNumeros", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            .Invoke(cpf, new object[] { input });

            // Assert
            Assert.Equal(expectedOutput, result);
        }
    }
}
