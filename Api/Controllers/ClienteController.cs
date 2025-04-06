using Application.UseCases.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ICadastrarClienteUseCase _cadastrarClienteUseCase;
        private readonly IObterClientePorCpfUseCase _obterClientePorCpfUseCase;

        public ClienteController(
            ICadastrarClienteUseCase cadastrarClienteUseCase,
            IObterClientePorCpfUseCase obterClientePorCpfUseCase)
        {
            _cadastrarClienteUseCase = cadastrarClienteUseCase;
            _obterClientePorCpfUseCase = obterClientePorCpfUseCase;
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        /// <param name="clienteDto">Dados do cliente a ser cadastrado</param>
        /// <returns>O cliente cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteDTO clienteDto)
        {
            if (clienteDto == null)
                return BadRequest("Os dados do cliente são obrigatórios.");

            try
            {
                var clienteCadastrado = await _cadastrarClienteUseCase.ExecutarAsync(clienteDto);
                return CreatedAtAction(nameof(ObterClientePorCpf), new { cpf = clienteCadastrado.Cpf }, clienteCadastrado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Obtém um cliente pelo CPF.
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>O cliente encontrado ou NotFound se não existir</returns>
        [HttpGet("{cpf}")]
        public async Task<IActionResult> ObterClientePorCpf(string cpf)
        {
            var cliente = await _obterClientePorCpfUseCase.ExecutarAsync(cpf);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            return Ok(cliente);
        }
    }

}
