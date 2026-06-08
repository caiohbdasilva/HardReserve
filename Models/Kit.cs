using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Models
{
    public class Kit
    {
        public int Id { get; set; } 
        public string NomeKit { get; set; }
        public string Descricao { get; set; }
        public int UsuarioCriadorId { get; set; }
        public string Localizacao { get; set; }
        public int Quantidade { get; set; }
    }
}