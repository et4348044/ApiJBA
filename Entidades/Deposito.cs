using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ApiJBA.Entidades
{
    [Table("depositos")]
    public class Deposito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_deposito { get; set; }

        [Column(TypeName = "varchar(100)")]
        [StringLength(100)]
        public string? nombre_d { get; set; }

        [Required]
        public DateTime fecha_registro { get; set; } = DateTime.Now;

        // Propiedades de navegación
        public ICollection<StockDeposito> StockDepositos { get; set; } = new List<StockDeposito>();
        public ICollection<DetalleColaboracion> DetalleColaboraciones { get; set; } = new List<DetalleColaboracion>();
    }
}
