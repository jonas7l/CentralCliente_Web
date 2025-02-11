using ClienteAplicacao.Interfaces;
using ClienteDominio.Entidades;
using ClienteInfraestrutura.Interfaces;

namespace ClienteAplicacao.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _repositorio;

        public ClienteServico(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> CriarCliente(Cliente cliente)
        {
            return await _repositorio.CriarCliente(cliente);
        }

        public async Task<List<Cliente>> ObterClientes()
        {
            return await _repositorio.ObterClientes();
        }

        public async Task<bool> AtualizarCliente(Cliente cliente) 
        {
            return await _repositorio.AtualizarCliente(cliente);
        }

        public async Task<bool> DeletarCliente(Guid clienteId)
        {
            return await _repositorio.DeletarCliente(clienteId);
        }
    }
}
