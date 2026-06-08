using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Models
{
    public class Kit
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string NomeKit { get; set; } = null!;
        public string? Descricao { get; set; }

        [Required]
        public int UsuarioCriadorId { get; set; }

        public string Localizacao { get; set; } = null!;

        [Required]
        public int Quantidade { get; set; }
    }
}