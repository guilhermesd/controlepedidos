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

    // Busca um cliente pelo ID
    public async Task<Cliente> ObterPorIdAsync(int Id)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == Id);
    }

    // Adiciona um novo cliente
    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    // Atualiza um cliente existente
    public async Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    // Remove um cliente existente
    public async Task RemoverAsync(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
}
