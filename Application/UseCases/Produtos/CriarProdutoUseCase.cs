using AutoMapper;
using Crosscutting.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Produtos
{
    public class CriarProdutoUseCase : ICriarProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public CriarProdutoUseCase(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoDTO> Executar(ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);
            var produtoCriado = await _produtoRepository.CriarProdutoAsync(produto);
            return _mapper.Map<ProdutoDTO>(produtoCriado);
        }
    }

    public interface ICriarProdutoUseCase
    {
        Task<ProdutoDTO> Executar(ProdutoDTO produto);
    }
}
