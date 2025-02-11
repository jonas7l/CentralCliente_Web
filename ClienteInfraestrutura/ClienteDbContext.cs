using ClienteDominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ClienteInfraestrutura
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}
