namespace Domain.Entities
{
    public enum PedidoEtapa
    {
        Recebido,
        EmPreparacao,
        Pronto,
        Finalizado
    }

    public class Pedido
    {
        public int Id { get; private set; }

        public int? ClienteId { get; private set; }

        public PedidoEtapa? Etapa { get; private set; }

        public Cliente Cliente { get; private set; }

        public IEnumerable<Produto> Produtos { get; private set; }

        // Relacionamento 1:N (Um Pedido pode ter vários Pagamentos)
        public List<Pagamento> Pagamentos { get; private set; } = new();
    }
}