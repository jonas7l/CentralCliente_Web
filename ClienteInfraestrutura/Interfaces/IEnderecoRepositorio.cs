using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClienteInfraestrutura.Interfaces
{
    public interface IEnderecoRepositorio
    {
        Task<IEnumerable<Endereco>> ObterEnderecosPorClienteIdAsync(Guid clienteId); // Nome correto do método
        Task<bool> AdicionarEnderecoAsync(Endereco endereco); // Nome correto do método
    }
}
