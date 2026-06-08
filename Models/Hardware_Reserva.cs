using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Models
{
    public class Hardware_Reserva
    {
        public int ReservaId { get; set; } 
        public int HardwareId { get; set; } 
        public int Quantidade { get; set; }
    }
}