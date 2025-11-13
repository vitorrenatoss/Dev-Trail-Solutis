using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_4___coleções_e_LINQ
{
    internal class ContaCorrente
    {
    public int NumeroConta { get; set; }
    public string Titular { get; set; }
    public decimal Saldo { get; set; }
    public string TipoConta { get; set; }
    public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

    public ContaCorrente(int numeroConta, string titular, string tipoConta, decimal saldo = 0)
    {
        this.NumeroConta = numeroConta;
        this.Titular = titular;
        this.TipoConta = tipoConta;
        this.Saldo = saldo;
        this.Transacoes = new List<Transacao>();
    }

    public void AdicionarTransacao(int numeroConta, string data, string tipo, decimal valor, string descricao)
    {
        Transacoes.Add(new Transacao(numeroConta, data, tipo, valor, descricao));
        Saldo += tipo == "Credito" ? valor : -valor;
    }

    public void ExibirExtrato()
    {
        Console.WriteLine($"EXTRATO DA CONTA {NumeroConta:D3} - {Titular}:\n{new string('*', 40)}");
        foreach (var transacao in Transacoes.OrderByDescending(t => t.Data))
        {
            Console.WriteLine($"{transacao.Data:dd/MM/yyyy HH:mm} | {transacao.Tipo} | {transacao.Valor.ToString("C", new CultureInfo("pt-BR"))} | {transacao.Descricao} |");
        }
        Console.WriteLine($"\nSaldo atual: {Saldo.ToString("C", new CultureInfo("pt-BR"))}");
        }
    }
}

