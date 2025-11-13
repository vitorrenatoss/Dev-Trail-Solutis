using BankSystem.Api.Contexts;
using BankSystem.Api.Entities;
using BankSystem.Api.DTOs.Inputs;
using BankSystem.Api.DTOs.Views;
using BankSystem.Api.DTOs.Queries;
using BankSystem.Api.DTOs.Updates;
using BankSystem.Api.Repositories;
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

/* adicionar no construtor depois: IClienteService clienteService*/
public class ContaController(IContaService contaService) : ControllerBase
{
    private readonly CultureInfo brazilianCulture = new("pt-BR");
    
    [HttpGet]
    public async Task<IActionResult> ListarContasAsync([FromQuery] ContaQuery parametros) 
    {
        try
        {
            var contasResponse = await contaService.ListarContasAsync(parametros);
            var totalCount = await contaService.TotalContasAsync();

            Response.Headers.Append("X-Pagination-Total-Count", totalCount.ToString());
            Response.Headers.Append("X-Pagination-Page-Size", parametros.PageSize.ToString());
            Response.Headers.Append("X-Pagination-Current-Page", parametros.PageNumber.ToString());

            return Ok(contasResponse);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno.");
        }

        
    }

    [HttpGet("numero/{NumeroConta:int}")]
    public async Task<IActionResult> GetContaByNumeroAsync(int NumeroConta)
    {
        var contaResponse = await contaService.GetContaByNumeroAsync(NumeroConta);
        if (contaResponse == null)
        {
            return NotFound($"Não foi encontrada uma conta com o número {NumeroConta} .");
        }

        return Ok(contaResponse);
    }


    [HttpPost]
    public async Task<IActionResult> CriarContaAsync([FromBody] ContaInputModel input)
    {
        try
        {
            var viewModel = await contaService.CriarContaAsync(input);

            return CreatedAtAction(nameof(GetContaByNumeroAsync), new { numeroConta = viewModel.NumeroConta }, viewModel);
        }
        catch (Exception ex)
        {
            //se errar o input CPF cliente pra associar à nova conta
            if (ex.Message.Contains("Cliente com CPF") && ex.Message.Contains("não encontrado"))
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(500, $"Ocorreu um erro interno ao criar a conta.");
        }
    }

    [HttpPut("numero/{NumeroConta:int}")] // mudar só atributos Enum
    public async Task<IActionResult> AtualizarStatusContaAsync(int NumeroConta, [FromBody] ContaUpdateModel input)
    {
        var contaResponse = await contaService.AtualizarStatusContaAsync(NumeroConta, input);

        if (contaResponse == null)
        {
            return NotFound($"Não foi encontrada uma conta com o número {NumeroConta} .");
        }
        
        return Ok(contaResponse);
    }


    [HttpPatch("numero/{numeroConta:int}/deposito")]
    public async Task<IActionResult> DepositoAsync(int numeroConta, [FromBody] SaqueDepositoInputModel input)
    {
        try
        {
            var contaResponse = await contaService.DepositoAsync(numeroConta, input);
            if (contaResponse == null)
            {
                return NotFound($"Não foi encontrada uma conta com número {numeroConta}.");
            }
            return Ok(contaResponse);
        }
        catch (Exception ex)
        {
            if(ex.Message.Contains("contas ativas") || ex.Message.Contains("zero"))
            {
                return NotFound(ex.Message);
            }
            return StatusCode(500, $"Ocorreu um erro interno ao processar a transação.");
        }
    }

    [HttpPatch("numero/{numeroConta:int}/saque")]
    public async Task<IActionResult> SaqueAsync(int numeroConta, [FromBody] SaqueDepositoInputModel input)
    {
        try
        {
            var contaResponse = await contaService.SaqueAsync(numeroConta, input);
            if (contaResponse == null)
            {
                return NotFound($"Não foi encontrada uma conta com número {numeroConta}.");
            }
            return Ok(contaResponse);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("contas ativas") || ex.Message.Contains("zero") || ex.Message.Contains("insuficiente"))
            {
                return NotFound(ex.Message);
            }
            return StatusCode(500, $"Ocorreu um erro interno ao processar a transação.");
        }
    }



    [HttpDelete("numero/{numeroConta:int}")]
    public async Task<IActionResult> DeletarContaAsync(int numeroConta)
    {
        var conta = await contaService.GetContaByNumeroAsync(numeroConta);
        if (conta == null) { return NotFound(); }
        
        return NoContent();
    }

}
