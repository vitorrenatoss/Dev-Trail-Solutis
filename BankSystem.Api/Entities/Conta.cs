using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.Api.Entities;

[Index(nameof(NumeroConta), IsUnique = true)]
public class Conta
{

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public int NumeroConta { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Saldo { get; set; }
    
    [Required]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    [Required]
    public Tipo Tipo { get; set; }

    [Required]
    public Status Status { get; set; }

    public Cliente? Cliente { get; set; }

    public Guid ClienteId { get; set; }

}
