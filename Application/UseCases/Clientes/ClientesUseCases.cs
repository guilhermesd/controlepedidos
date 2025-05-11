using Crosscutting.Exceptions;
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

    public interface ISalvarClienteUseCase
    {
        Task<ClienteDTO> ExecutarAsync(int Id, ClienteDTO clienteDto);
    }

    public interface IObterClientePorCpfUseCase
    {
        Task<ClienteDTO> ExecutarAsync(string cpf);
    }

    public interface IRemoverClienteUseCase
    {
        Task ExecutarAsync(int id);
    }

    public class SalvarClienteUseCase : ISalvarClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public SalvarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> ExecutarAsync(int Id, ClienteDTO clienteDto)
        {

            Cliente clientePersistido = null;   
            if (Id == 0)
            {
                var clienteExistente = await _clienteRepository.ObterPorCpfAsync(clienteDto.Cpf);
                if (clienteExistente != null)
                {
                    throw new InvalidOperationException("Já existe um cliente cadastrado com esse CPF.");
                }

                clientePersistido = new Cliente(clienteDto.Nome, clienteDto.Cpf, clienteDto.Email);
                await _clienteRepository.AdicionarAsync(clientePersistido);
            }

            if(Id > 0)
            {
                var clienteExistente = await _clienteRepository.ObterPorCpfAsync(clienteDto.Cpf);
                if (clienteExistente != null && clienteExistente.Id != Id)
                {
                    throw new InvalidOperationException("Já existe um cliente cadastrado com esse CPF.");
                }

                clientePersistido = await _clienteRepository.ObterPorIdAsync(Id);

                if (clientePersistido == null)
                    throw new NotFoundException("Não encontrado");

                clientePersistido.Update(clienteDto.Nome, clienteDto.Cpf, clienteDto.Email);
                await _clienteRepository.AtualizarAsync(clientePersistido);
            }

            return new ClienteDTO
            {
                Id = clientePersistido.Id,
                Nome = clientePersistido.Nome,
                Cpf = clientePersistido.Cpf.Numero,
                Email = clientePersistido.Email.Endereco,
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
                throw new NotFoundException("Não encontrado");

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf.Numero,
                Email = cliente.Email.Endereco
            };
        }
    }

    public class RemoverClienteUseCase : IRemoverClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public RemoverClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task ExecutarAsync(int id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new NotFoundException("Não encontrado");

            await _clienteRepository.RemoverAsync(cliente);
        }
    }
}