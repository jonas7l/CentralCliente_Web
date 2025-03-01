using System.ComponentModel.DataAnnotations;

public class EnderecoDto
{
    [Required]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "O Logradouro é obrigatório.")]
    [MinLength(3, ErrorMessage = "O logradouro deve ter pelo menos 3 caracteres.")]
    public string Logradouro { get; set; }
}
