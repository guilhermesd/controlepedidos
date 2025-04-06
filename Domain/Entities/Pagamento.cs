namespace Domain.Entities
{
    public class Pagamento
    {
        public int Id { get; private set; }

        public string IdentificadorPagamento { get; private set; }

        public int PedidoId { get; private set; }

        public Pedido Pedido { get; private set; } = null!;

        /// <summary>
        /// Nulo - Aguardando
        /// true - Aprovado
        /// false - Reprovado
        /// </summary>
        public bool? Aprovado { get; private set; }

        public Pagamento(int pedidoId, string identificadorPagamento)
        {
            PedidoId = pedidoId;
            IdentificadorPagamento = identificadorPagamento;
        }

        public void Aprovar(bool aprova) => Aprovado = aprova;
    }
}