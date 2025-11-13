using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using BankSystem.Api.Entities;

namespace BankSystem.Api.DTOs.Views; 
public class ContaViewModel 
{
    public Guid Id { get; set; }
    public int NumeroConta { get; set; }
    public decimal SaldoAtual { get; set; }
    public DateTime DataCriacao { get; set; }

    public string NomeCliente { get; set; } = string.Empty;


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Tipo Tipo { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; }
}

