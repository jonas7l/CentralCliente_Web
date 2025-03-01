using Microsoft.AspNetCore.Mvc;
using ClienteAplicacao.Interfaces;
using System;
using System.Threading.Tasks;

namespace ClienteAPI.Controllers
{
    [Route("api/enderecos")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoServico _servico;

        public EnderecoController(IEnderecoServico servico)
        {
            _servico = servico;
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObterEnderecos(Guid clienteId)
        {
            //var enderecos = await _servico.ObterEnderecosPorClienteId(clienteId);
            //if (enderecos == null) return NotFound("Nenhum endereço encontrado para esse cliente.");

            //return Ok(enderecos);
            var enderecos = await _servico.ObterEnderecosPorClienteId(clienteId);

            var enderecosDto = enderecos.Select(e => new EnderecoDto
            {
                ClienteId = e.ClienteId,
                Logradouro = e.Logradouro
            });

            return Ok(enderecosDto);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEndereco([FromBody] EnderecoDto enderecoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sucesso = await _servico.AdicionarEndereco(enderecoDto.ClienteId, enderecoDto.Logradouro);
            if (!sucesso) return BadRequest("Erro ao adicionar endereço.");

            return CreatedAtAction(nameof(ObterEnderecos), new { clienteId = enderecoDto.ClienteId }, "Endereço adicionado com sucesso!");
        }
    }
}
