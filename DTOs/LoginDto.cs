using System.ComponentModel.DataAnnotations;

namespace ApiJBA.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "La cédula (ci_p) es requerida para iniciar sesión.")]
        [StringLength(20, ErrorMessage = "La cédula no puede superar los 20 caracteres.")]
        public string ci_p { get; set; } = default!;
    }
}
