using Application.UseCases.Produtos;
using Crosscutting.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly ISalvarProdutoUseCase _salvarProdutoUseCase;
        private readonly IRemoverProdutoUseCase _removerProdutoUseCase;
        private readonly IObterProdutosUseCase _obterProdutosUseCase;

        public ProdutoController(
            ISalvarProdutoUseCase salvarProdutoUseCase,
            IRemoverProdutoUseCase removerProdutoUseCase,
            IObterProdutosUseCase obterProdutosUseCase)
        {
            _salvarProdutoUseCase = salvarProdutoUseCase;
            _removerProdutoUseCase = removerProdutoUseCase;
            _obterProdutosUseCase = obterProdutosUseCase;
        }

        /// <summary>
        /// Cria ou atualiza um produto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] ProdutoDTO produtoDto)
        {
            var produtoCriado = await _salvarProdutoUseCase.Executar(0, produtoDto);
            return CreatedAtAction(nameof(ObterProdutos), new { id = produtoCriado.Id }, produtoCriado);
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoDTO produtoDto)
        {
            await _salvarProdutoUseCase.Executar(id, produtoDto); 
            return NoContent(); 
        }

        /// <summary>
        /// Remove um produto.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            await _removerProdutoUseCase.ExecutarAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Obtém produtos com filtros opcionais.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObterProdutos([FromQuery] ObterProdutosDTO filtro)
        {
            var produtos = await _obterProdutosUseCase.ExecutarAsync(filtro);
            return Ok(produtos);
        }
    }
}
