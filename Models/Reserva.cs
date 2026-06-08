using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Models
{
    public class Reserva
    {
        public int Id { get; set; } // PK
        public int UsuarioId { get; set; } // FK
        public int? TurmaId { get; set; } // FK
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string StatusReserva { get; set; } = "PE"; // Padrão: Pendente
    }
}