using System.ComponentModel.DataAnnotations;

namespace TesteSmartHint.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome ou Razão Social é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        public string Telefone { get; set; }

        public string? InscricaoEstadual { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool FlgAtivo { get; set; }

        public bool FlgInscricaoEstadualPF { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ é obrigatório.")]
        public string Cpf_Cnpj { get; set; }

        public string? Genero { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data de nascimento deve ser uma data válida.")]
        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "Tipo de pessoa é obrigatório.")]
        public string TipoPessoa { get; set; }
    }
}
