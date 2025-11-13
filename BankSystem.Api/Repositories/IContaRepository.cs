using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.Entities;
namespace BankSystem.Api.Repositories;

public interface IContaRepository
{
    public Task<IEnumerable<Conta>> ListarContasAsync(ContaQuery parametros);
    public Task<Conta?> GetContaByNumeroAsync(int numeroConta);
    public Task<Conta?> GetContaByIdAsync(Guid id);

    public Task<int> GetTotalContasAsync();
    public Task<int?> GetMaxNumeroContaAsync();

    public Task AddAsync(Conta conta);
    public Task<Conta?> GetContaForUpdateAsync(int numeroConta);
    public void AtualizarStatusConta(Conta conta);
    public Task<bool> SaveChangesAsync();

    public Task<Cliente?> GetClienteByCPFAsync(string CPF);
}
