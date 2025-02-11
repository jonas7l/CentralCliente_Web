using ClienteInfraestrutura.Interfaces;
using ClienteDominio.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteInfraestrutura.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ClienteDbContext _context;

        public ClienteRepositorio(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CriarCliente(Cliente cliente)
        {
            if (await _context.Clientes.AnyAsync(c => c.Email == cliente.Email))
            {
                throw new Exception("E-mail já está em uso.");
            }

            using var connection = _context.Database.GetDbConnection() as SqlConnection;
            if (connection == null) return false;

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_InserirCliente", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Logotipo", (object?)cliente.Logotipo ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<List<Cliente>> ObterClientes()
        {
            return await _context.Clientes
                .Include(c => c.Enderecos) 
                .ToListAsync();
        }

        public async Task<bool> AtualizarCliente(Cliente cliente) // ✅ Implementação correta
        {
            using var connection = _context.Database.GetDbConnection() as SqlConnection;
            if (connection == null) return false;

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_AtualizarCliente", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", cliente.Id);
            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Logotipo", (object?)cliente.Logotipo ?? DBNull.Value);

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeletarCliente(Guid clienteId)
        {
            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
