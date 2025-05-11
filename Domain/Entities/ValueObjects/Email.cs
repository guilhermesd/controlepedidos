using System.Text.RegularExpressions;

namespace Domain.Entities.ValueObjects
{
    public class Email
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("E-mail não pode ser nulo ou vazio.", nameof(endereco));

            endereco = endereco.Trim();

            if (endereco.Length > 254)
                throw new ArgumentException("E-mail excede o tamanho máximo de 254 caracteres.", nameof(endereco));

            if (!EmailRegex.IsMatch(endereco))
                throw new ArgumentException("Formato de e-mail inválido.", nameof(endereco));

            Endereco = endereco;
        }

        public override string ToString() => Endereco;

        public override bool Equals(object obj)
        {
            return obj is Email other && Endereco.Equals(other.Endereco, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Endereco.ToLowerInvariant().GetHashCode();
        }
    }
}
