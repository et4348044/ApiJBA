using System;

namespace ApiJBA.DTOs
{
    public class CreacionDePersonal_Get_DTO
    {
        public string ci_p { get; set; } = default!;
        public string nombre_p { get; set; } = default!;
        public int nivel { get; set; }
        public int estado { get; set; }
        public string cargo { get; set; } = default!;
        public DateTime fr_p { get; set; }
        public DateTime? fs_p { get; set; }
        public string dir_p { get; set; } = default!;
        public string? correo_p { get; set; }
        public string NroCuenta { get; set; } = default!;
        public byte[]? Archivo { get; set; }
    }
}
