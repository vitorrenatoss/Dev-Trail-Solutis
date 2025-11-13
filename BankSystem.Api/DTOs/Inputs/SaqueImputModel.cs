using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace BankSystem.Api.DTOs.Inputs;

public class SaqueInputModel
{
    [Required(ErrorMessage = "O valor do saque é obrigatório.")]
    [Range(0.01, 100000.00, ErrorMessage = "O valor do saque deve ser positivo e no máximo R$ 100.000,00")]
    public decimal Valor { get; set; }
}

