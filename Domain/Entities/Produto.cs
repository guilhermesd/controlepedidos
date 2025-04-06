using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum CategoriaProduto
    {
        Lanche,
        Acompanhamento,
        Bebida,
        Sobremesa
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UrlImagem { get; set; }
        public decimal Preco { get; set; }
        public CategoriaProduto Categoria { get; set; }
    }
}