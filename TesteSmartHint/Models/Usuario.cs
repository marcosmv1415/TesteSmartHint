﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteSmartHint.Libraries.Lang;
namespace TesteSmartHint.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Telefone { get; set; }
        public bool FlgAtivo { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Genero { get; set; }
        public DateTime DataNascimento { get; set; }
    }
    
}
