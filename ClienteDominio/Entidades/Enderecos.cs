using System.ComponentModel.DataAnnotations;

public class Enderecos
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "O Logradouro é obrigatório.")]
    public string Logradouro { get; set; }

    public Clientes Cliente { get; set; }
}
