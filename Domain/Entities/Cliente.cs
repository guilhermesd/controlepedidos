using Domain.Entities.ValueObjects;

namespace Domain.Entities
{
    public class Cliente
    {
        private Cliente() { }

        public int Id { get; private set; }
        public string Nome { get; private set; } 

        // Propriedade que contém o Value Object CPF
        public Cpf Cpf { get; private set; }

        // Propriedade que contém o Value Object EMAIL
        public Email Email { get; private set; }

        public Cliente(string nome, string cpf, string email)
        {
            Update(nome, cpf, email);
        }

        public void Update(string nome, string cpf, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do cliente não pode ser vazio ou nulo.", nameof(nome));

            Nome = nome;
            Cpf = new Cpf(cpf);
            Email = new Email(email);
        }
    }
}