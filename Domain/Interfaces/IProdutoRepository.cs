using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> CriarProdutoAsync(Produto produto);
        Task<List<Produto>> ObterProdutosAsync();
    }
}
