using ClienteInfraestrutura.Interfaces;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClienteInfraestrutura;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly ClienteDbContext _context;

    public ClienteRepositorio(ClienteDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CriarCliente(Clientes cliente)
    {
        if (await _context.Clientes.AnyAsync(c => c.Email == cliente.Email))
        {
            throw new Exception("O e-mail já está em uso.");
        }

        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Clientes>> ObterClientes()
    {
        return await _context.Clientes
            .Include(c => c.Enderecos)  // 🔹 Busca os logradouros corretamente através dos endereços
            .ToListAsync();
    }

    public async Task<Clientes?> ObterClientePorId(Guid clienteId) 
    {
        return await _context.Clientes
            .Include(c => c.Enderecos)
            .FirstOrDefaultAsync(c => c.Id == clienteId);
    }

    public async Task<bool> AtualizarCliente(Clientes cliente)
    {
        var clienteExistente = await _context.Clientes.FindAsync(cliente.Id);
        if (clienteExistente == null)
        {
            return false;
        }

        clienteExistente.Nome = cliente.Nome;
        clienteExistente.Email = cliente.Email;
        clienteExistente.Logotipo = cliente.Logotipo;

        _context.Clientes.Update(clienteExistente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarCliente(Guid clienteId)
    {
        var cliente = await _context.Clientes.FindAsync(clienteId);
        if (cliente == null)
        {
            return false;
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}
