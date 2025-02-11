using ClienteDominio.Entidades;

namespace ClienteAplicacao.Interfaces
{
    public interface IClienteServico
    {
        Task<bool> CriarCliente(Cliente cliente);
        Task<List<Cliente>> ObterClientes();
        Task<bool> AtualizarCliente(Cliente cliente); 
        Task<bool> DeletarCliente(Guid clienteId);
    }
}
