using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Models
{
    public class Hardware
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string QuantidadeTotal { get; set; }
        public string Localizacao { get; set; }
        public int? KitId { get; set; }
    }
}