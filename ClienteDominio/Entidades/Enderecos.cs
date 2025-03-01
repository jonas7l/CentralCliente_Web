using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Enderecos
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "O Logradouro é obrigatório.")]
    public string Logradouro { get; set; }

    [JsonIgnore] // Evita ciclo infinito na serialização
    public Clientes Cliente { get; set; }
}




