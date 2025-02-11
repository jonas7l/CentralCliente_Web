using ClienteAplicacao.Interfaces;

using ClienteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

[Authorize]
[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteServico _servico;
    private readonly IMemoryCache _cache;


    public ClientesController(IClienteServico servico, IMemoryCache cache)
    {
        _servico = servico;
        _cache = cache;
    }
    
    [HttpGet]
    public async Task<IActionResult> ObterClientes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var clientes = await _servico.ObterClientes();
        var pagedClientes = clientes.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Ok(new
        {
            TotalClientes = clientes.Count,
            PaginaAtual = page,
            TamanhoPagina = pageSize,
            Clientes = pagedClientes
        });
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente([FromForm] ClienteDto cliente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var clienteObj = new Clientes
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
        if (id == Guid.Empty)
        {
            return BadRequest("ID do cliente inválido.");
        }

        var clienteExistente = await _servico.ObterClientePorId(id);
        if (clienteExistente == null)
        {
            return NotFound("Cliente não encontrado.");
        }

        clienteExistente.Nome = cliente.Nome;
        clienteExistente.Email = cliente.Email;

        if (cliente.Logotipo != null)
        {
            clienteExistente.Logotipo = ConvertToByteArray(cliente.Logotipo);
        }

        // Atualizar os logradouros
        if (cliente.Logradouros != null && cliente.Logradouros.Count > 0)
        {
            clienteExistente.Enderecos.Clear();
            clienteExistente.Enderecos.AddRange(cliente.Logradouros.Select(logradouro => new Endereco
            {
                ClienteId = id,
                Logradouro = logradouro
            }));
        }

        var sucesso = await _servico.AtualizarCliente(clienteExistente);
        if (!sucesso)
            return BadRequest("Erro ao atualizar cliente.");

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarCliente(Guid id)
    {
        var cliente = await _servico.ObterClientePorId(id);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }

        var sucesso = await _servico.DeletarCliente(id);
        if (!sucesso)
        {
            return BadRequest("Erro ao deletar cliente.");
        }

        return NoContent();
    }


    private byte[] ConvertToByteArray(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();
    }
}
