using BankSystem.Api.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Api.DTOs.Updates;

public class ContaUpdateModel
{
    [Required(ErrorMessage = "O tipo da conta é obrigatório.")]
    [EnumDataType(typeof(Tipo), ErrorMessage = "O tipo de conta fornecido é inválido.")]
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public Tipo Tipo { get; set; }

    [Required(ErrorMessage = "O status da conta é obrigatório.")]
    [EnumDataType(typeof(Status), ErrorMessage = "O status fornecido é inválido.")]
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public Status Status { get; set; }
}
