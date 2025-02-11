using ClienteDominio.Entidades;

namespace ClienteInfraestrutura.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<bool> CriarCliente(Cliente cliente);
        Task<List<Cliente>> ObterClientes();
        Task<bool> AtualizarCliente(Cliente cliente);
        Task<bool> DeletarCliente(Guid clienteId);
    }
}
