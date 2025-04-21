using Crosscutting.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> CriarProdutoAsync(Produto produto);

        Task<bool> AtualizarProdutoAsync(Produto produtoAtualizado);

        Task<List<Produto>> ObterProdutosAsync(ObterProdutosDTO obterProdutosDTO);

        Task<bool> ExcluirProdutoAsync(int id);
    }
}
