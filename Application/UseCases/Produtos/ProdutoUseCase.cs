using AutoMapper;
using Crosscutting.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Produtos
{
    public interface ISalvarProdutoUseCase
    {
        Task<ProdutoDTO> Executar(int id, ProdutoDTO produto);
    }

    public interface IRemoverProdutoUseCase
    {
        Task ExecutarAsync(int id);
    }

    public interface IObterProdutosUseCase
    {
        Task<List<ProdutoDTO>> ExecutarAsync(ObterProdutosDTO filtro);
    }

    public class ObterProdutosUseCase : IObterProdutosUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ObterProdutosUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoDTO>> ExecutarAsync(ObterProdutosDTO filtro)
        {
            var produtos = await _produtoRepository.ObterProdutosAsync(filtro);

            return produtos.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                UrlImagem = p.UrlImagem,
                Preco = p.Preco,
                Categoria = (CategoriaProdutoDTO)p.Categoria
            }).ToList();
        }
    }

    public class SalvarProdutoUseCase : ISalvarProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public SalvarProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoDTO> Executar(int id, ProdutoDTO produtoDTO)
        {
            Produto produtoPersistido = null;
            if (id == 0)
            {
                var produto = new Produto(
                        produtoDTO.Nome,
                        produtoDTO.Descricao,
                        produtoDTO.UrlImagem,
                        produtoDTO.Preco,
                        (CategoriaProduto)produtoDTO.Categoria);

                produtoPersistido = await _produtoRepository.CriarProdutoAsync(produto);
            }

            if (id > 0)
            {
                produtoPersistido = await _produtoRepository.ObterProdutosPorIdAsync(id);
                if (produtoPersistido == null)
                    throw new InvalidOperationException("Produto não encontrado.");

                produtoPersistido.Update(
                        produtoDTO.Nome, 
                        produtoDTO.Descricao, 
                        produtoDTO.UrlImagem, 
                        produtoDTO.Preco,
                        (CategoriaProduto)produtoDTO.Categoria);

                await _produtoRepository.AtualizarProdutoAsync(produtoPersistido);
            }

            return new ProdutoDTO
            {
                Categoria = (CategoriaProdutoDTO) produtoPersistido.Categoria,
                Descricao = produtoPersistido.Descricao,
                Id = produtoPersistido.Id,
                Nome = produtoPersistido.Nome,
                Preco = produtoPersistido.Preco,
                UrlImagem = produtoPersistido.UrlImagem
            };
        }
    }

    public class RemoverProdutoUseCase : IRemoverProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public RemoverProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task ExecutarAsync(int id)
        {
            var produto = await _produtoRepository.ObterProdutosPorIdAsync(id);
            if (produto == null)
                throw new InvalidOperationException("Produto não encontrado.");

            await _produtoRepository.ExcluirProdutoAsync(id);
        }
    }
}