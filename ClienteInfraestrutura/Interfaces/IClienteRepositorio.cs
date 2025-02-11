

namespace ClienteInfraestrutura.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<bool> CriarCliente(Clientes cliente);
        Task<List<Clientes>> ObterClientes();
        Task<Clientes?> ObterClientePorId(Guid clienteId); // ✅ Novo método para buscar um cliente pelo ID
        Task<bool> AtualizarCliente(Clientes cliente);
        Task<bool> DeletarCliente(Guid clienteId);
    }
}
