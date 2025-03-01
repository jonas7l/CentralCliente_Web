using ClienteInfraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteInfraestrutura.Repositorios
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private readonly ClienteDbContext _context;

        public EnderecoRepositorio(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Endereco>> ObterEnderecosPorClienteIdAsync(Guid clienteId)
        {
            return await _context.Enderecos
                .Where(e => e.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<bool> AdicionarEnderecoAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
