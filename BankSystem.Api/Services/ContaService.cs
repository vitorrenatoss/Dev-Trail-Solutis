using BankSystem.Api.Contexts;
using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.Entities;
using BankSystem.Api.Repositories;
using BankSystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystem.Api.Services;

public class ContaService(IContaRepository repository) : IContaService
{
    //método auxiliar pras views
    private ContaViewModel MapToViewModel(Conta conta)
    {
        return new ContaViewModel
        {
            Id = conta.Id,
            NumeroConta = conta.NumeroConta,
            SaldoAtual = conta.Saldo,
            Tipo = conta.Tipo,
            Status = conta.Status,
            DataCriacao = conta.DataCriacao,
            NomeCliente = conta.Cliente?.Nome ?? "N/A",
        };
    }
    public async Task<IEnumerable<ContaViewModel>> ListarContasAsync(ContaQuery parametros)
    {
        var contasPaginadas = await repository.ListarContasAsync(parametros);

        var viewModels = contasPaginadas.Select(MapToViewModel).ToList();

        return viewModels;
    }

    public async Task<int> TotalContasAsync()
    {
        return await repository.GetTotalContasAsync();
    }

    public async Task<ContaViewModel?> GetContaByNumeroAsync(int numeroConta)
    {
        var conta = await repository.GetContaByNumeroAsync(numeroConta);
        if (conta == null) {  return null; }
        return MapToViewModel(conta);
    }

    public async Task<ContaViewModel?> GetContaByIdAsync(Guid id)
    {
        var conta = await repository.GetContaByIdAsync(id);
        if (conta == null) { return null; }
        return MapToViewModel(conta);
    }

    public async Task<ContaViewModel> CriarContaAsync(ContaInputModel input)
    {
        var cliente = await repository.GetClienteByCPFAsync(input.CPF);

        if (cliente == null)
        {
            // Lançamos uma exceção que o Controller irá capturar
            throw new Exception($"Cliente com CPF {input.CPF} não encontrado.");
        }

        int novoNumeroConta;
        const int numeroContaInicial = 0000; 
        var maxNumeroConta = await repository.GetMaxNumeroContaAsync();
        novoNumeroConta = maxNumeroConta.HasValue ? maxNumeroConta.Value + 1 : numeroContaInicial;

        var novaConta = new Conta
        {
            DataCriacao = DateTime.Now, 
            NumeroConta = novoNumeroConta,
            Saldo = input.Saldo,
            Tipo = input.Tipo,
            Status = Status.Ativa, //
            Cliente = cliente,
            ClienteId = cliente.Id,
        };

        await repository.AddAsync(novaConta);
        await repository.SaveChangesAsync();

        return MapToViewModel(novaConta);
    }

    public async Task<ContaViewModel?> AtualizarStatusContaAsync(int numeroConta, ContaUpdateModel input)
    {
       var conta = await repository.GetContaForUpdateAsync(numeroConta);
        if (conta == null)
        {
            return null;
        }
        conta.Tipo = input.Tipo;
        conta.Status = input.Status;

        repository.AtualizarStatusConta(conta);
        await repository.SaveChangesAsync();

        return MapToViewModel(conta);
    }

    public async Task<ContaViewModel?> DepositoAsync(int numeroConta, SaqueDepositoInputModel input)
    {
        var conta = await repository.GetContaForUpdateAsync(numeroConta);
        if (conta == null) { return null; }

        if (conta.Status != Status.Ativa)
        {
            throw new Exception("Depósitos só podem ser realizados em contas ativas.");
        }

        if (input.Valor <= 0)
        {
            throw new Exception("O valor do depósito deve ser maior que zero.");
        }

        conta.Saldo += input.Valor;
        await repository.SaveChangesAsync();
        return MapToViewModel(conta);
    }

    public async Task<ContaViewModel?> SaqueAsync(int numeroConta, SaqueDepositoInputModel input)
    {
        var conta = await repository.GetContaForUpdateAsync(numeroConta);
        if (conta == null) { return null; }
        if (conta.Status != Status.Ativa)
        {
            throw new Exception("Saques só podem ser realizados em contas ativas.");
        }
        if (input.Valor <= 0)
        {
            throw new Exception("O valor do saque deve ser maior que zero.");
        }
        if (conta.Saldo < input.Valor)
        {
            throw new Exception("Saldo insuficiente para realizar o saque.");
        }
        conta.Saldo -= input.Valor;
        await repository.SaveChangesAsync();
        return MapToViewModel(conta);
    }


    public async Task<bool> DeletarContaAsync(int numeroConta)
    {
        var conta = await repository.GetContaForUpdateAsync(numeroConta);
        if (conta == null) { return false; }

        //verificação extra pra não exigir mais das outras camadas
        if (conta.Status == Status.Encerrada) { return true; }

        conta.Status = Status.Encerrada;

        repository.AtualizarStatusConta(conta);
        return await repository.SaveChangesAsync();

    }

}
