namespace HardReserve.Models
{
    public class Usuario
    {
        public int Id { get; set; } // PK - Identity
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public char Status_Usuario { get; set; } = 'D'; // 'D' = Disponível, 'I' = Indisponível
        public char Role { get; set; } // 'A' = Aluno, 'P' = Professor, 'T' = Técnico
        public string Turma_Usuario { get; set; } = null!;
    }
}