using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClienteAplicacao.Interfaces
{
    public interface IEnderecoServico
    {
        Task<IEnumerable<Endereco>> ObterEnderecosPorClienteId(Guid clienteId);
        Task<bool> AdicionarEndereco(Guid clienteId, string logradouro);
    }
}
