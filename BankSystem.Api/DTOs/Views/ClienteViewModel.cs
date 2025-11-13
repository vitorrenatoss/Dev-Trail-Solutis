using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using BankSystem.Api.Entities;
using System;
using System.Collections.Generic;
namespace BankSystem.Api.DTOs.Views;


public class ClienteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ContaViewModel>? Contas { get; set; } = new List<ContaViewModel>();
}
