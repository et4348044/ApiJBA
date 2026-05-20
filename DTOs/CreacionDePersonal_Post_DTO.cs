using System;
using System.ComponentModel.DataAnnotations;

namespace ApiJBA.DTOs
{
    public class CreacionDePersonal_Post_DTO
    {
        [Required]
        [StringLength(20)]
        public string ci_p { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string nombre_p { get; set; } = default!;

        [Required]
        [Range(1, 10, ErrorMessage = "El nivel debe estar entre 1 y 10 (1=Personal General, 6=Vocero, 7=Secretaria, 8=Subdirector, 9=Director, 10=Sistemas).")]
        public int nivel { get; set; }

        [Required]
        public int estado { get; set; }

        [StringLength(25)]
        public string? cargo { get; set; } // Puede ser nulo en la petición para que el backend lo asigne automáticamente según el nivel

        [Required]
        public DateTime fr_p { get; set; }

        public DateTime? fs_p { get; set; }

        [Required]
        [StringLength(120)]
        public string dir_p { get; set; } = default!;

        [StringLength(50)]
        public string? correo_p { get; set; }

        [Required]
        [StringLength(255)]
        public string NroCuenta { get; set; } = default!;

        public byte[]? Archivo { get; set; } // Representa el archivo/imagen (se envía en formato Base64 en el JSON)
    }
}
