using BankSystem.Api.Contexts;
using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.Entities;
using BankSystem.Api.Repositories;
using System.Threading.Tasks;

namespace BankSystem.Api.Services;

public interface IClienteService
{
    public Task<int> GetTotalClientesAsync();
    public Task<IEnumerable<ClienteViewModel>> ListarClientesAsync(ClienteQuery parametros);
    public Task<ClienteViewModel?> GetClienteByCPFAsync(string cpf);
    public Task<IEnumerable<ContaViewModel>?> GetContasByClienteCPFAsync(string cpf);
    Task<ClienteViewModel> CriarClienteAsync(ClienteInputModel input);
}
