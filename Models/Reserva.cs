using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HardReserve.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; } // FK
        [ForeignKey("UsuarioId")]
        [Required]
        public Usuario Usuario { get; set; } = null!;

        [Required]
        public DateTime DataInicial { get; set; } 

        [Required]
        public DateTime DataFinal { get; set; }

        [Required]
        public string StatusReserva { get; set; } = "PE";

        [Required]
        public int Quantidade { get; set; }
    }
}