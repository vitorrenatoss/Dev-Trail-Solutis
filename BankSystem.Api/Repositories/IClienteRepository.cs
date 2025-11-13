using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.Entities;
namespace BankSystem.Api.Repositories;

public interface IClienteRepository
{

    public Task<int> GetTotalClientesAsync();
    public Task<bool> CpfExistenteAsync(string cpf);
    public Task AddAsync(Cliente cliente);
    //public Task<Cliente?> GetClienteForUpdateAsync(string cpf);
    public void AtualizarStatusCliente(Cliente cliente);
    public Task<bool> SaveChangesAsync();
    public Task<Cliente?> GetClienteByCPFAsync(string cpf);
    public Task<Cliente?> GetClienteComContasAsync(string cpf);
    public Task <IEnumerable<Cliente>> ListarClientesAsync(ClienteQuery parametros);
}
