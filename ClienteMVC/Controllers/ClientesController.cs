using ClienteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class ClientesController : Controller
{
    private readonly HttpClient _httpClient;

    public ClientesController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7055/api/clientes");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("");
        var json = await response.Content.ReadAsStringAsync();
        var clientes = JsonSerializer.Deserialize<List<ClienteDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ViewBag.Clientes = clientes;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Criar(ClienteDto cliente)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(cliente.Nome), "Nome");
        content.Add(new StringContent(cliente.Email), "Email");

        foreach (var logradouro in cliente.Logradouros)
        {
            content.Add(new StringContent(logradouro), "Logradouros");
        }

        if (cliente.Logotipo != null)
        {
            var fileStream = cliente.Logotipo.OpenReadStream();
            content.Add(new StreamContent(fileStream), "Logotipo", cliente.Logotipo.FileName);
        }

        var response = await _httpClient.PostAsync("", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Erro ao criar cliente.");
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Atualizar(ClienteDto cliente)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(cliente.Id.ToString()), "Id");
        content.Add(new StringContent(cliente.Nome), "Nome");
        content.Add(new StringContent(cliente.Email), "Email");

        foreach (var logradouro in cliente.Logradouros)
        {
            content.Add(new StringContent(logradouro), "Logradouros");
        }

        if (cliente.Logotipo != null)
        {
            var fileStream = cliente.Logotipo.OpenReadStream();
            content.Add(new StreamContent(fileStream), "Logotipo", cliente.Logotipo.FileName);
        }

        var response = await _httpClient.PutAsync($"{cliente.Id}", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Erro ao atualizar cliente.");
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _httpClient.DeleteAsync($"{id}");
        return RedirectToAction("Index");
    }

}
