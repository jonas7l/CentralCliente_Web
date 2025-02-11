using ClienteAplicacao.Interfaces;
using ClienteDominio.Entidades;
using ClienteMVC.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteServico _servico;

    public ClientesController(IClienteServico servico)
    {
        _servico = servico;
    }

    [HttpGet]
    public async Task<IActionResult> ObterClientes()
    {
        var clientes = await _servico.ObterClientes();

        var resultado = clientes.Select(cliente => new
        {
            cliente.Id,
            cliente.Nome,
            cliente.Email,
            Logradouros = cliente.Enderecos?.Select(e => e.Logradouro).ToList() ?? new List<string>() 
        });

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente([FromForm] ClienteDto cliente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var clienteObj = new Cliente
        {
            Nome = cliente.Nome,
            Email = cliente.Email,
            Logotipo = cliente.Logotipo != null ? ConvertToByteArray(cliente.Logotipo) : null,
            Enderecos = new List<Endereco>()
        };

        var sucesso = await _servico.CriarCliente(clienteObj);
        if (!sucesso)
            return BadRequest("Erro ao criar cliente.");

        return CreatedAtAction(nameof(ObterClientes), new { id = clienteObj.Id }, clienteObj);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarCliente(Guid id, [FromForm] ClienteDto cliente)
    {
        var clienteObj = new Cliente
        {
            Id = id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Logotipo = cliente.Logotipo != null ? ConvertToByteArray(cliente.Logotipo) : null
        };

        var sucesso = await _servico.AtualizarCliente(clienteObj);
        if (!sucesso)
            return NotFound("Cliente não encontrado.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarCliente(Guid id)
    {
        var sucesso = await _servico.DeletarCliente(id);
        if (!sucesso)
            return NotFound("Cliente não encontrado.");

        return NoContent();
    }

    private byte[] ConvertToByteArray(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();
    }
}
