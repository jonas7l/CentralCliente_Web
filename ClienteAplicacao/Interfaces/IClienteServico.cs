

namespace ClienteAplicacao.Interfaces
{
    public interface IClienteServico
    {
        Task<bool> CriarCliente(Clientes cliente);
        Task<List<Clientes>> ObterClientes();
        Task<Clientes?> ObterClientePorId(Guid clienteId); 
        Task<bool> AtualizarCliente(Clientes cliente);
        Task<bool> DeletarCliente(Guid clienteId);
    }
}
