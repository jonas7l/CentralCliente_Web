using System.ComponentModel.DataAnnotations;

public class Clientes
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; }

    public byte[]? Logotipo { get; set; }

    // 🔹 RELAÇÃO CORRETA: Um cliente pode ter vários endereços
    public List<Endereco>? Enderecos { get; set; } = new List<Endereco>();
}

public class Endereco
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "O Logradouro é obrigatório.")]
    public string Logradouro { get; set; }

    public Clientes Cliente { get; set; }  // 🔹 Relacionamento com Cliente
}
