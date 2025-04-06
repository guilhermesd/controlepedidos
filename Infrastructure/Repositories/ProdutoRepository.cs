using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Produto>> ObterProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        // Método para obter produtos por categoria
        public async Task<List<Produto>> ObterProdutosPorCategoriaAsync(CategoriaProduto categoria)
        {
            return await _context.Produtos
                                 .Where(p => p.Categoria == categoria)
                                 .ToListAsync();
        }

        // Método para obter um produto por ID
        public async Task<Produto> ObterProdutoPorIdAsync(int id)
        {
            return await _context.Produtos
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Método para atualizar um produto existente
        public async Task<Produto> AtualizarProdutoAsync(int id, Produto produtoAtualizado)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return null;

            produto.Nome = produtoAtualizado.Nome;
            produto.Descricao = produtoAtualizado.Descricao;
            produto.UrlImagem = produtoAtualizado.UrlImagem;
            produto.Preco = produtoAtualizado.Preco;
            produto.Categoria = produtoAtualizado.Categoria;

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        // Método para excluir um produto
        public async Task<bool> ExcluirProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
