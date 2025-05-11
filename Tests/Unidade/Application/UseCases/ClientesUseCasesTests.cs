using Application.UseCases.Clientes;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unidade.Application.UseCases
{
    public class ClientesUseCasesTests
    {
        [Fact]
        public async Task SalvarCliente_DeveLancarNotFoundException_QuandoClienteNaoExiste()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync((Cliente)null);
            mockRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Cliente)null);

            var useCase = new SalvarClienteUseCase(mockRepo.Object);

            var clienteDto = new ClienteDTO
            {
                Id = 1,
                Nome = "Teste",
                Cpf = "12345678900",
                Email = "teste@email.com"
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecutarAsync(1, clienteDto));
        }

        [Fact]
        public async Task ObterClientePorCpf_DeveLancarNotFoundException_QuandoCpfNaoExiste()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync((Cliente)null);

            var useCase = new ObterClientePorCpfUseCase(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecutarAsync("12345678900"));
        }

        [Fact]
        public async Task RemoverCliente_DeveLancarNotFoundException_QuandoClienteNaoExiste()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Cliente)null);

            var useCase = new RemoverClienteUseCase(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecutarAsync(99));
        }

    }
}
