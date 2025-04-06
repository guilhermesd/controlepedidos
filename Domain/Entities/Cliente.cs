using Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente
    {
        private Cliente() { }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        // Navegação para os Pedidos
        public ICollection<Pedido> Pedidos { get; private set; }

        // Propriedade que contém o Value Object CPF
        public Cpf Cpf { get; set; }

        public string NumeroCpf { get; set; }

        public Cliente(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = new Cpf(cpf);
            Email = email;
        }
    }
}
