public class ClienteDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public IFormFile? Logotipo { get; set; }
    public List<string>? Logradouros { get; set; } 
}
