using ClienteAplicacao.Interfaces;
using ClienteInfraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClienteAplicacao.Servicos
{
    public class EnderecoServico : IEnderecoServico
    {
        private readonly IEnderecoRepositorio _repositorio;

        public EnderecoServico(IEnderecoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Endereco>> ObterEnderecosPorClienteId(Guid clienteId)
        {
            return await _repositorio.ObterEnderecosPorClienteIdAsync(clienteId);
        }

        public async Task<bool> AdicionarEndereco(Guid clienteId, string logradouro)
        {
            var endereco = new Endereco
            {
                Id = Guid.NewGuid(),
                ClienteId = clienteId,
                Logradouro = logradouro
            };

            return await _repositorio.AdicionarEnderecoAsync(endereco);
        }
    }
}
