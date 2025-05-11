using Api.Controllers;
using Application.UseCases.Clientes;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechTalk.SpecFlow;

namespace Tests.BDD
{
    [Binding]
    public class CadastrarClienteSteps
    {
        private readonly ClienteDTO _clienteDto;
        private readonly ClienteDTO _clienteRetorno;
        private readonly Mock<ISalvarClienteUseCase> _mockSalvarCliente;
        private readonly Mock<IObterClientePorCpfUseCase> _mockObterCliente;
        private readonly Mock<IRemoverClienteUseCase> _mockRemoverCliente;
        private ClienteController _controller;
        private IActionResult _resultado;

        public CadastrarClienteSteps()
        {
            _clienteDto = new ClienteDTO { Cpf = "88539239035", Nome = "João", Email = "erer@ere.com" };
            _clienteRetorno = new ClienteDTO { Cpf = "57301923040", Nome = "João", Email = "ererrtr@eretrt.com" };

            _mockSalvarCliente = new Mock<ISalvarClienteUseCase>();
            _mockObterCliente = new Mock<IObterClientePorCpfUseCase>();
            _mockRemoverCliente = new Mock<IRemoverClienteUseCase>();
        }

        [Given("que um cliente válido foi informado")]
        public void DadoQueUmClienteValidoFoiInformado()
        {
            _mockSalvarCliente
                .Setup(x => x.ExecutarAsync(0, _clienteDto))
                .ReturnsAsync(_clienteRetorno);

            _controller = new ClienteController(_mockSalvarCliente.Object, _mockObterCliente.Object, _mockRemoverCliente.Object);
        }

        [When("o cliente for cadastrado")]
        public async Task QuandoOClienteForCadastrado()
        {
            _resultado = await _controller.CadastrarCliente(_clienteDto);
        }

        [Then("o cliente deve ser retornado com status 201")]
        public void EntaoOClienteDeveSerRetornadoComStatus201()
        {
            var createdResult = _resultado as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(_clienteRetorno, createdResult.Value);
        }
    }
}
