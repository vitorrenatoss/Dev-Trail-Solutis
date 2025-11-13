using Microsoft.AspNetCore.Mvc;
using BankSystem.Api.Contexts;
using BankSystem.Api.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BankSystem.Api.DTOs.Inputs;

public class ClienteInputModel : IValidatableObject
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression("^[0-9]{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
    public string CPF { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
    public string Email { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - DataNascimento.Year;
        
        if (DataNascimento > hoje.AddYears(-idade)) { idade--; };

        if (idade < 18)
            {
            yield return new ValidationResult("O cliente deve ter pelo menos 18 anos para abrir uma conta.", new[] { nameof(DataNascimento) });
        }

    }
}
