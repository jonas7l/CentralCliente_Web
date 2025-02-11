using System.ComponentModel.DataAnnotations;

namespace ClienteMVC.Models
{
    public class ClienteDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        public List<string> Logradouros { get; set; } = new List<string>();

        public IFormFile? Logotipo { get; set; }

        public byte[]? LogotipoBytes { get; set; } 

        public string? LogotipoBase64 => LogotipoBytes != null ? Convert.ToBase64String(LogotipoBytes) : null; 
    }
}
