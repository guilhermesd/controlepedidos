using Crosscutting.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para criar um novo produto
        public async Task<Produto> CriarProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        // Método para obter todos os produtos
        public async Task<List<Produto>> ObterProdutosAsync(ObterProdutosDTO obterProdutosDTO)
        {
            var query = _context.Produtos.AsQueryable();

            if (obterProdutosDTO.Id.HasValue)
            {
                query = query.Where(p => p.Id == obterProdutosDTO.Id.Value);
            }

            if (obterProdutosDTO.Categoria.HasValue)
            {
                query = query.Where(p => p.Categoria == (CategoriaProduto)obterProdutosDTO.Categoria.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produtoAtualizado)
        {
            // Update properties directly  
            _context.Produtos.Update(produtoAtualizado);
            await _context.SaveChangesAsync();
            return true;
        }

        // Método para excluir um produto
        public async Task<bool> ExcluirProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
