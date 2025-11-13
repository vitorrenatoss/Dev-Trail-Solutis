using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.Entities;
using BankSystem.Api.Repositories;
using BankSystem.Api.Contexts;

namespace BankSystem.Api.Services;

public class ClienteService(IClienteRepository repository) : IClienteService
{
    private ClienteViewModel MapToViewModel(Cliente cliente)
    {
        return new ClienteViewModel
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            CPF = cliente.CPF,
            Email = cliente.Email,
            DataNascimento = cliente.DataNascimento,
            Contas = new List<ContaViewModel>()
        };
    }

    private ContaViewModel MapContaToViewModel(Conta conta)
    {
        return new ContaViewModel
        {
            Id = conta.Id,
            SaldoAtual = conta.Saldo,
            Tipo = conta.Tipo,
            Status = conta.Status,
            DataCriacao = conta.DataCriacao,
            NomeCliente = conta.Cliente?.Nome ?? string.Empty,
        };
    }

    public async Task<int> GetTotalClientesAsync() { return await repository.GetTotalClientesAsync(); }

    public async Task<IEnumerable<ClienteViewModel>> ListarClientesAsync(ClienteQuery parametros)
    {
        var clientesPaginados = await repository.ListarClientesAsync(parametros);
        var viewModels = clientesPaginados.Select(MapToViewModel).ToList();
        return (viewModels);
    }

    public async Task<ClienteViewModel?> GetClienteByCPFAsync(string cpf)
    {
        var cliente = await repository.GetClienteByCPFAsync(cpf);
        if (cliente == null) { return null; }
        return MapToViewModel(cliente);
    }

    public async Task<IEnumerable<ContaViewModel>?> GetContasByClienteCPFAsync(string cpf)
    {
        var cliente = await repository.GetClienteComContasAsync(cpf);
        if (cliente == null) { return null; }
        var contasViewModel = cliente.Contas.Select(MapContaToViewModel).ToList();
        return contasViewModel;
    }

    public async Task<ClienteViewModel> CriarClienteAsync(ClienteInputModel input)
    {
        var cpfCheck = await repository.CpfExistenteAsync(input.CPF);
        if (cpfCheck)
        {
            throw new Exception($"Já existe um cliente cadastrado com este CPF.");
        }

        var novoCliente = new Cliente
        {
            Nome = input.Nome,
            CPF = input.CPF,
            DataNascimento = input.DataNascimento,
            Email = input.Email,
        };

        await repository.AddAsync(novoCliente);
        await repository.SaveChangesAsync();
        return MapToViewModel(novoCliente);

    }
}
