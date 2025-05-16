using Api.Controllers;
using Application.UseCases.Clientes;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integracao.Controller
{
    public class ClienteControllerTests
    {
        private AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TesteDb")
                .Options; 

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarCreated()
        {
            // Arrange
            var context = CriarContextoInMemory();
            var repo = new ClienteRepository(context);
            var useCaseSalvar = new SalvarClienteUseCase(repo);
            var useCaseObter = new ObterClientePorCpfUseCase(repo);
            var useCaseRemover = new RemoverClienteUseCase(repo);

            var controller = new ClienteController(useCaseSalvar, useCaseObter, useCaseRemover);

            var clienteDto = new ClienteDTO
            {
                Nome = "Maria da Silva",
                Cpf = "31960926829",
                Email = "maria@email.com"
            };

            // Act
            var resultado = await controller.CadastrarCliente(clienteDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var clienteRetornado = Assert.IsType<ClienteDTO>(createdResult.Value);
            Assert.Equal(clienteDto.Cpf, clienteRetornado.Cpf);
        }

        [Fact]
        public async Task AlterarCliente_ShouldReturnNoContent_WhenClientIsUpdated()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var clienteRepository = new ClienteRepository(context);
                var salvarClienteUseCase = new SalvarClienteUseCase(clienteRepository);
                var obterClientePorCpfUseCase = new ObterClientePorCpfUseCase(clienteRepository);
                var removerClienteUseCase = new RemoverClienteUseCase(clienteRepository);

                var controller = new ClienteController(salvarClienteUseCase, obterClientePorCpfUseCase, removerClienteUseCase);

                var clienteDto = new ClienteDTO { Nome = "João", Cpf = "31960926829", Email = "joao@exemplo.com" };
                var clienteCadastrado = await controller.CadastrarCliente(clienteDto) as CreatedAtActionResult;
                var clienteCadastradoDto = clienteCadastrado?.Value as ClienteDTO;

                // Act
                var updatedClienteDto = new ClienteDTO { Nome = "João Atualizado", Cpf = "31960926829", Email = "joaoatualizado@exemplo.com" };
                var result = await controller.AlterarCliente(clienteCadastradoDto?.Id??0, updatedClienteDto);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task DeletarCliente_ShouldReturnNoContent_WhenClientIsDeleted()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var clienteRepository = new ClienteRepository(context);
                var salvarClienteUseCase = new SalvarClienteUseCase(clienteRepository);
                var obterClientePorCpfUseCase = new ObterClientePorCpfUseCase(clienteRepository);
                var removerClienteUseCase = new RemoverClienteUseCase(clienteRepository);

                var controller = new ClienteController(salvarClienteUseCase, obterClientePorCpfUseCase, removerClienteUseCase);

                var clienteDto = new ClienteDTO { Nome = "João", Cpf = "31960926829", Email = "joao@exemplo.com" };
                var clienteCadastrado = await controller.CadastrarCliente(clienteDto) as CreatedAtActionResult;
                var clienteCadastradoDto = clienteCadastrado?.Value as ClienteDTO;

                // Act
                var result = await controller.DeletarCliente(clienteCadastradoDto?.Id??0);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task ObterClientePorCpf_DeveRetornarCliente_SeCpfExistir()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var nome = faker.Name.FullName();
            var cpf = faker.Person.Cpf(false); // Gera CPF válido como string
            var email = faker.Internet.Email();

            var context = CriarContextoInMemory();

            var cliente = new Cliente(
                nome: nome,
                cpf: cpf,
                email: email
            );

            context.Clientes.Add(cliente);
            context.SaveChanges();

            var repo = new ClienteRepository(context);
            var useCaseSalvar = new SalvarClienteUseCase(repo);
            var useCaseObter = new ObterClientePorCpfUseCase(repo);
            var useCaseRemover = new RemoverClienteUseCase(repo);

            var controller = new ClienteController(useCaseSalvar, useCaseObter, useCaseRemover);

            // Act
            var resultado = await controller.ObterClientePorCpf(cpf);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var clienteResult = Assert.IsType<ClienteDTO>(okResult.Value);
            Assert.Equal(nome, clienteResult.Nome);
            Assert.Equal(cpf, clienteResult.Cpf);
            Assert.Equal(email, clienteResult.Email);
        }
    }
}