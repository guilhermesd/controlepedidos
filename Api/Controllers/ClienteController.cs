using Application.UseCases.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ISalvarClienteUseCase _salvarClienteUseCase;
        private readonly IObterClientePorCpfUseCase _obterClientePorCpfUseCase;
        private readonly IRemoverClienteUseCase _removerClienteUseCase;

        public ClienteController(
            ISalvarClienteUseCase salvarClienteUseCase,
            IObterClientePorCpfUseCase obterClientePorCpfUseCase,
            IRemoverClienteUseCase removerClienteUseCase)
        {
            _salvarClienteUseCase = salvarClienteUseCase;
            _obterClientePorCpfUseCase = obterClientePorCpfUseCase;
            _removerClienteUseCase = removerClienteUseCase;
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        /// <param name="clienteDto">Dados do cliente a ser cadastrado</param>
        /// <returns>O cliente cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteDTO clienteDto)
        {
            var clienteCadastrado = await _salvarClienteUseCase.ExecutarAsync(0, clienteDto);
            return CreatedAtAction(nameof(ObterClientePorCpf), new { cpf = clienteCadastrado.Cpf }, clienteCadastrado);
        }

        /// <summary>
        /// Altera os dados de um cliente existente.
        /// </summary>
        /// <param name="Id">ID do cliente a ser alterado</param>
        /// <param name="clienteDto">Dados atualizados do cliente</param>
        /// <returns>204 No Content em caso de sucesso</returns>
        [HttpPut("{Id}")]
        public async Task<IActionResult> AlterarCliente(int Id, [FromBody] ClienteDTO clienteDto)
        {
            await _salvarClienteUseCase.ExecutarAsync(Id, clienteDto);
            return NoContent(); // 204 - sucesso, sem retorno de conteúdo
        }

        /// <summary>
        /// Deleta um cliente existente.
        /// </summary>
        /// <param name="Id">ID do cliente a ser alterado</param>
        /// <returns>204 No Content em caso de sucesso</returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletarCliente(int Id)
        {
            await _removerClienteUseCase.ExecutarAsync(Id);
            return NoContent(); // 204 - sucesso, sem retorno de conteúdo
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
            return Ok(cliente);
        }
    }
}