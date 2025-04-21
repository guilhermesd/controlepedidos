using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    // Busca um cliente pelo CPF
    public async Task<Cliente> ObterPorCpfAsync(string cpf)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
    }

    // Adiciona um novo cliente
    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }
}
