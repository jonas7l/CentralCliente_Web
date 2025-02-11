using System.ComponentModel.DataAnnotations;

namespace ClienteDominio.Entidades
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        public byte[]? Logotipo { get; set; }
        public List<Endereco>? Enderecos { get; set; } = new List<Endereco>(); 
    }

    public class Endereco
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        [StringLength(255, ErrorMessage = "O Logradouro deve ter no máximo 255 caracteres.")]
        public string Logradouro { get; set; }
    }
}
