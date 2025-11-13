using BankSystem.Api.Contexts;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystem.Api.Repositories;

public class ContaRepository(BankContext context) : IContaRepository
{
    public async Task<int> GetTotalContasAsync()
    {
        return await context.Contas.AsNoTracking().CountAsync();
    }

    public async Task<int?> GetMaxNumeroContaAsync()
    {
        bool any = await context.Contas.AsNoTracking().AnyAsync();
        if (!any)
        {
            return null; 
        }
        return await context.Contas.MaxAsync(c => c.NumeroConta);
    }

    public async Task AddAsync(Conta conta)
    {
        await context.Contas.AddAsync(conta);
    }

    public void AtualizarStatusConta(Conta conta)
    {
        context.Contas.Update(conta);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Conta>> ListarContasAsync(ContaQuery parametros)
    {
        var query = context.Contas
            .Include(c => c.Cliente)
            .AsNoTracking();
        
        query = AplicarOrdenacao(query, parametros.OrderBy);

        var contasPaginadas = await query
            .Skip((parametros.PageNumber - 1) * parametros.PageSize)
            .Take(parametros.PageSize)
            .ToListAsync();

        return contasPaginadas;
    }

    private IQueryable<Conta> AplicarOrdenacao(IQueryable<Conta> query, string criterio)
    {
        //ver se precisa verificar tipo inválido, também
        if (string.IsNullOrWhiteSpace(criterio))
        {
            return query.OrderBy(c => c.DataCriacao); // Padrão
        }

        var order = criterio.ToLowerInvariant();

        return order switch
        {
            "saldo" => query.OrderBy(c => c.Saldo),
            "saldodesc" => query.OrderByDescending(c => c.Saldo),
            "datacriacao" => query.OrderBy(c => c.DataCriacao),
            "datacriacaodesc" => query.OrderByDescending(c => c.DataCriacao),
            "numeroconta" => query.OrderBy(c => c.NumeroConta),
            "numerocontadesc" => query.OrderByDescending(c => c.NumeroConta),
            _ => throw new ArgumentException($"O parâmetro de ordenação '{criterio}' é inválido.")
        };
    }

    public async Task<Conta?> GetContaByNumeroAsync(int numeroConta)
    {
        return await context.Contas
            .Include(c => c.Cliente)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);
    }

    public async Task<Conta?> GetContaByIdAsync(Guid id)
    {
        return await context.Contas
            .Include(c => c.Cliente)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Conta?> GetContaForUpdateAsync(int numeroConta)
    {
        return await context.Contas
            .Include(c => c.Cliente)
            .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);
    }


    public async Task<Cliente?> GetClienteByCPFAsync(string CPF)
    {
        return await context.Clientes
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.CPF == CPF);
    }

}

