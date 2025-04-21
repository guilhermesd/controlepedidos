namespace Crosscutting.DTOs
{
    public enum CategoriaProdutoDTO
    {
        Lanche,
        Acompanhamento,
        Bebida,
        Sobremesa
    }

    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public CategoriaProdutoDTO Categoria { get; set; }
    }
}
