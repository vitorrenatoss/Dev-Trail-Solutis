using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.Repositories;
using BankSystem.Api.Contexts;
using BankSystem.Api.Entities;

namespace BankSystem.Api.Services;



public interface IContaService
{
    Task<IEnumerable<ContaViewModel>> ListarContasAsync(ContaQuery parametros);
    Task<int> TotalContasAsync();
    Task<ContaViewModel?> GetContaByNumeroAsync(int numeroConta);
    Task<ContaViewModel?> GetContaByIdAsync(Guid id);

    Task<ContaViewModel> CriarContaAsync(ContaInputModel input);
    Task<ContaViewModel?> AtualizarStatusContaAsync(int numeroConta, ContaUpdateModel input);
    Task<ContaViewModel?> DepositoAsync(int numeroConta, SaqueDepositoInputModel input);
    Task<ContaViewModel?> SaqueAsync(int numeroConta, SaqueDepositoInputModel input);
    Task<bool> DeletarContaAsync(int numeroConta);
}
