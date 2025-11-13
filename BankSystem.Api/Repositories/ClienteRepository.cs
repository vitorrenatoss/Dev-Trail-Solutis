using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.Entities;
using BankSystem.Api.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Api.Repositories;

public class ClienteRepository(BankContext context) : IClienteRepository
{
    public async Task<int> GetTotalClientesAsync() 
    { return await context.Clientes.AsNoTracking().CountAsync(); }
    public async Task<bool> CpfExistenteAsync(string cpf)
    { return await context.Clientes.AnyAsync(c => c.CPF == cpf); }
    public async Task<Cliente?> GetClienteByCPFAsync(string cpf)
    { return await context.Clientes.FirstOrDefaultAsync(c => c.CPF == cpf); }
    public async Task AddAsync(Cliente cliente)
    { await context.Clientes.AddAsync(cliente); }
    public void AtualizarStatusCliente(Cliente cliente)
    {context.Clientes.Update(cliente);}
    public async Task<bool> SaveChangesAsync()
    { return await context.SaveChangesAsync() > 0; }
    public async Task <IEnumerable<Cliente>> ListarClientesAsync(ClienteQuery parametros)
    {
        var query = context.Clientes.AsNoTracking().AsQueryable();

        query = AplicarOrdenacao(query, parametros.OrderBy, parametros.SortDirection);

        var clientesPaginados = await query
            .Skip((parametros.PageNumber - 1) * parametros.PageSize)
            .Take(parametros.PageSize)
            .ToListAsync();     

        return clientesPaginados;
    }

    public async Task<Cliente?> GetClienteComContasAsync(string cpf)
    {
        return await context.Clientes
            .Include(c => c.Contas)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CPF == cpf);
    }


    private IQueryable<Cliente> AplicarOrdenacao(IQueryable<Cliente> query, string orderBy, string sortDirection)
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            return query.OrderBy(c => c.Nome); // Padrão
        }

        var direction = sortDirection.ToLower() == "desc" ? "desc" : "asc";

        return orderBy.ToLower() switch
        {
            "nome" => direction == "desc"
                ? query.OrderByDescending(c => c.Nome)
                : query.OrderBy(c => c.Nome),

            "cpf" => direction == "desc"
                ? query.OrderByDescending(c => c.CPF)
                : query.OrderBy(c => c.CPF),

            _ => query.OrderBy(c => c.Nome) // Ordenação padrão, caso o campo não seja válido
        };
    }
}
