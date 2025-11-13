using BankSystem.Api.Contexts;
using BankSystem.Api.Entities;
using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.Services;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Threading.Tasks;
namespace BankSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController(IClienteService clienteService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> ListarClientes([FromQuery] ClienteQuery parametros)
    {
        var viewModels = await clienteService.ListarClientesAsync(parametros);
        var total = await clienteService.GetTotalClientesAsync();

        // 2. Adicionar os Headers de Paginação
        Response.Headers.Add("X-Total-Count", total.ToString());
        Response.Headers.Add("X-Page-Number", parametros.PageNumber.ToString());
        Response.Headers.Add("X-Page-Size", parametros.PageSize.ToString());

        // 3. Retornar 200 OK com os dados
        return Ok(viewModels);
    }


    [HttpGet("{CPF}")]
    public async Task<IActionResult> GetClienteByCPFAsync(string CPF)
    {
        var viewModel = await clienteService.GetClienteByCPFAsync(CPF);
        if (viewModel == null)
        { return NotFound($"Cliente com CPF {CPF} não encontrado."); }
        return Ok(viewModel);
    }

    [HttpGet("{CPF}/contas")]
    public async Task<IActionResult> GetContasByCliente([FromRoute] string CPF)
    {
        var contasResponse = await clienteService.GetContasByClienteCPFAsync(CPF);
        if (contasResponse == null)
        {
            return NotFound($"Cliente com CPF {CPF} não encontrado.");
        }

        return Ok(contasResponse);
    }


    [HttpPost]
    public async Task<IActionResult> CriarClienteAsync([FromBody] ClienteInputModel input)
    {
        try
        {
            var viewModel = await clienteService.CriarClienteAsync(input);

            return CreatedAtAction(nameof(GetClienteByCPFAsync), new { CPF = viewModel.CPF }, viewModel);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Já existe um cliente cadastrado com este CPF"))
            {
                return Conflict(ex.Message);
            }

            return StatusCode(500, "Ocorreu um erro interno ao salvar o cliente.");
        }
    }
}
