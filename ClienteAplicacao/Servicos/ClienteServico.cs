using ClienteAplicacao.Interfaces;

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

        public async Task<bool> CriarCliente(Clientes cliente)
        {
            // Verifica se já existe um cliente com o mesmo e-mail
            var clientesExistentes = await _repositorio.ObterClientes();
            if (clientesExistentes.Any(c => c.Email == cliente.Email))
            {
                throw new Exception("O e-mail já está cadastrado para outro cliente.");
            }

            return await _repositorio.CriarCliente(cliente);
        }

        public async Task<List<Clientes>> ObterClientes()
        {
            var clientes = await _repositorio.ObterClientes();
            return clientes ?? new List<Clientes>(); // Retorna lista vazia caso não haja clientes
        }

        public async Task<Clientes?> ObterClientePorId(Guid clienteId)
        {
            return await _repositorio.ObterClientePorId(clienteId);
        }

        public async Task<bool> AtualizarCliente(Clientes cliente)
        {
            var clienteExistente = await _repositorio.ObterClientePorId(cliente.Id);
            if (clienteExistente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Email = cliente.Email;

            if (cliente.Logotipo != null)
            {
                clienteExistente.Logotipo = cliente.Logotipo;
            }

            clienteExistente.Enderecos.Clear();
            clienteExistente.Enderecos.AddRange(cliente.Enderecos);

            return await _repositorio.AtualizarCliente(clienteExistente);
        }

        public async Task<bool> DeletarCliente(Guid clienteId)
        {
            var clienteExistente = await _repositorio.ObterClientePorId(clienteId);
            if (clienteExistente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            return await _repositorio.DeletarCliente(clienteId);
        }
    }
}
