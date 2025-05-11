using Api.Controllers;
using Application.UseCases.Produtos;
using Crosscutting.DTOs;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Tests.Integracao.Controller
{
    public class ProdutoControllerTests: IClassFixture<Program>
    {
        private AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TesteDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CriarProduto_ShouldReturnCreated_WhenProdutoIsCreated()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb_CriarProduto")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var produtoRepository = new ProdutoRepository(context);
                var salvarProdutoUseCase = new SalvarProdutoUseCase(produtoRepository);
                var removerProdutoUseCase = new RemoverProdutoUseCase(produtoRepository);
                var obterProdutosUseCase = new ObterProdutosUseCase(produtoRepository);

                var controller = new ProdutoController(salvarProdutoUseCase, removerProdutoUseCase, obterProdutosUseCase);

                var produtoDto = new ProdutoDTO
                {
                    Nome = "Produto Teste",
                    Descricao = "Descrição do produto",
                    UrlImagem = "https://imagem.com/produto.jpg",
                    Preco = 99.99m,
                    Categoria = CategoriaProdutoDTO.Lanche
                };

                var result = await controller.CriarProduto(produtoDto);

                var createdAt = Assert.IsType<CreatedAtActionResult>(result);
                var produtoCriado = Assert.IsType<ProdutoDTO>(createdAt.Value);
                Assert.Equal("Produto Teste", produtoCriado.Nome);
                Assert.Equal(CategoriaProdutoDTO.Lanche, produtoCriado.Categoria);
            }
        }

        [Fact]
        public async Task AtualizarProduto_ShouldReturnNoContent_WhenProdutoIsUpdated()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb_AtualizarProduto")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var produtoRepository = new ProdutoRepository(context);
                var salvarProdutoUseCase = new SalvarProdutoUseCase(produtoRepository);
                var removerProdutoUseCase = new RemoverProdutoUseCase(produtoRepository);
                var obterProdutosUseCase = new ObterProdutosUseCase(produtoRepository);

                var controller = new ProdutoController(salvarProdutoUseCase, removerProdutoUseCase, obterProdutosUseCase);

                var produtoDto = new ProdutoDTO
                {
                    Nome = "Produto Original",
                    Descricao = "Original",
                    UrlImagem = "",
                    Preco = 10,
                    Categoria = CategoriaProdutoDTO.Bebida
                };

                var criado = await controller.CriarProduto(produtoDto) as CreatedAtActionResult;
                var produtoCriado = criado.Value as ProdutoDTO;

                var atualizadoDto = new ProdutoDTO
                {
                    Nome = "Produto Atualizado",
                    Descricao = "Atualizado",
                    UrlImagem = "",
                    Preco = 20,
                    Categoria = CategoriaProdutoDTO.Sobremesa
                };

                var result = await controller.AtualizarProduto(produtoCriado.Id, atualizadoDto);

                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task RemoverProduto_ShouldReturnNoContent_WhenProdutoIsDeleted()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb_RemoverProduto")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var produtoRepository = new ProdutoRepository(context);
                var salvarProdutoUseCase = new SalvarProdutoUseCase(produtoRepository);
                var removerProdutoUseCase = new RemoverProdutoUseCase(produtoRepository);
                var obterProdutosUseCase = new ObterProdutosUseCase(produtoRepository);

                var controller = new ProdutoController(salvarProdutoUseCase, removerProdutoUseCase, obterProdutosUseCase);

                var produtoDto = new ProdutoDTO
                {
                    Nome = "Produto Teste",
                    Descricao = "Teste",
                    UrlImagem = "",
                    Preco = 50,
                    Categoria = CategoriaProdutoDTO.Acompanhamento
                };

                var criado = await controller.CriarProduto(produtoDto) as CreatedAtActionResult;
                var produtoCriado = criado.Value as ProdutoDTO;

                var result = await controller.RemoverProduto(produtoCriado.Id);

                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task ObterProdutos_DeveRetornarProdutosFiltradosPorCategoria()
        {
            // Arrange
            var context = CriarContextoInMemory();
            var produtoRepository = new ProdutoRepository(context);
            var salvarProdutoUseCase = new SalvarProdutoUseCase(produtoRepository);
            var removerProdutoUseCase = new RemoverProdutoUseCase(produtoRepository);
            var obterProdutosUseCase = new ObterProdutosUseCase(produtoRepository);

            var controller = new ProdutoController(salvarProdutoUseCase, removerProdutoUseCase, obterProdutosUseCase);

            var produtoDto = new ProdutoDTO
            {
                Nome = "Produto Original",
                Descricao = "Original",
                UrlImagem = "",
                Preco = 10,
                Categoria = CategoriaProdutoDTO.Bebida
            };

            var criado = await controller.CriarProduto(produtoDto) as CreatedAtActionResult;

            var filtro = new ObterProdutosDTO
            {
                Categoria = CategoriaProdutoDTO.Bebida,
                Id = (criado?.Value as ProdutoDTO)?.Id??0
            };

            // Act
            var resultado = await controller.ObterProdutos(filtro);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var produtos = Assert.IsAssignableFrom<IEnumerable<ProdutoDTO>>(okResult.Value);
            var produto = produtos.Single();

            Assert.Equal(CategoriaProdutoDTO.Bebida, produto.Categoria);
            Assert.Equal(filtro.Id, produto.Id);
        }
    }
}