using BankSystem.Api.Contexts;
using BankSystem.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Api.DTOs.Inputs;

public class ContaInputModel
{
    [Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter entre 11 e 14 caracteres.")]
    public string CPF { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ClienteId é obrigatório.")]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "O saldo inicial é obrigatório.")]
    [Range(0.01, 999999.00, ErrorMessage = "O saldo deve ser positivo e menor que R$ 999.999,00")]
    public decimal Saldo { get; set; }

    [Required(ErrorMessage = "O tipo da conta é obrigatório.")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(Tipo), ErrorMessage = "O tipo de conta fornecido é inválido.")]
    public Tipo Tipo { get; set; }
}

