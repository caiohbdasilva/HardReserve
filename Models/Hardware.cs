using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Models
{
    public class Hardware
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [StringLength(255)]
        public string? Descricao { get; set; }

        [Required]
        public int Quantidade_Total { get; set; }

        [StringLength(100)]
        public string? Localizacao { get; set; }

        public int? Kit_Id { get; set; }

        [StringLength(50)]
        public string? Categoria { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "disponivel";

        [StringLength(50)]
        public string? Codigo_Patrimonio { get; set; }

        public string? Imagem { get; set; }

        [NotMapped]
        public int QuantidadeDisponivel { get; set; }

        [NotMapped]
        public int QuantidadeNaReserva { get; set; }
    }
}
