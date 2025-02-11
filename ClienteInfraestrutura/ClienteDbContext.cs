
using Microsoft.EntityFrameworkCore;

public class ClienteDbContext : DbContext
{
    public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options) { }

    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
}
