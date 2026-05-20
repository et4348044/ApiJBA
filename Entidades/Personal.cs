using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiJBA.Entidades
{
    [Table("personal")]
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // La cédula se ingresa manualmente, no es auto-incremental
        [Required]
        [StringLength(20)]
        public string ci_p { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string nombre_p { get; set; } = default!;

        [Required]
        public int nivel { get; set; }

        [Required]
        public int estado { get; set; }

        [Required]
        [StringLength(25)]
        public string cargo { get; set; } = default!;

        [Required]
        public DateTime fr_p { get; set; }

        public DateTime? fs_p { get; set; } // Puede ser nulo si no ha renunciado

        [Required]
        [StringLength(120)]
        public string dir_p { get; set; } = default!;

        [StringLength(50)]
        public string? correo_p { get; set; } // Puede ser nulo si no tiene

        [Required]
        [StringLength(255)]
        public string NroCuenta { get; set; } = default!;

        [Column(TypeName = "image")] // Mapea al tipo de datos legacy 'image' en SQL Server
        public byte[]? Archivo { get; set; }
    }
}
