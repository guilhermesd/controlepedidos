using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Clientes
{

    public class ClienteDTO
    {
        public int? Id { get; set; } // Opcional para retorno de busca
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
    }

    public interface ICadastrarClienteUseCase
    {
        Task<ClienteDTO> ExecutarAsync(ClienteDTO clienteDto);
    }

    public interface IObterClientePorCpfUseCase
    {
        Task<ClienteDTO> ExecutarAsync(string cpf);
    }


    public class CadastrarClienteUseCase : ICadastrarClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public CadastrarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> ExecutarAsync(ClienteDTO clienteDto)
        {
            var clienteExistente = await _clienteRepository.ObterPorCpfAsync(clienteDto.Cpf);
            if (clienteExistente != null)
            {
                throw new InvalidOperationException("Já existe um cliente cadastrado com esse CPF.");
            }

            var cliente = new Cliente(clienteDto.Nome, clienteDto.Cpf, clienteDto.Email);
            await _clienteRepository.AdicionarAsync(cliente);

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf.Numero
            };
        }
    }

    public class ObterClientePorCpfUseCase : IObterClientePorCpfUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientePorCpfUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO?> ExecutarAsync(string cpf)
        {
            var cliente = await _clienteRepository.ObterPorCpfAsync(cpf);
            if (cliente == null)
                return null;

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf.Numero
            };
        }
    }

}
