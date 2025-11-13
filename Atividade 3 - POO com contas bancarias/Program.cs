using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_3___POO_com_contas_bancarias
{
    public class Program
    {
        private static CultureInfo CulturaBrasileira = new CultureInfo("pt-BR");

        static void Main()
        {
            Console.WriteLine("=== Teste de Classes de Contas Bancárias ===");
            Console.WriteLine();
            CultureInfo.CurrentCulture = CulturaBrasileira;
            TestarContaCorrente();
            TestarContaEspecial();
            TestarContaPoupanca();
            Console.WriteLine("\n=== Fim dos Testes ===");
        }

    

        private static void EscreverResultado(string operacao, decimal valor, bool sucesso)
        {
            string resultado = sucesso ? "SUCESSO" : "FALHA";
            Console.WriteLine($"  -> {resultado}: {operacao} de {valor.ToString("C", CulturaBrasileira)}.");
        }


        private static void TestarContaCorrente()
        {
            Console.WriteLine("\n## 1. Testando Conta Corrente ##");
            ContaCorrente cc = new ContaCorrente("João da Silva", "001-CC");
            Console.WriteLine($"Conta: {cc.Titular} ({cc.NumeroConta})");
            cc.ConsultarSaldo(); 

            
            decimal valorDeposito1 = 500.00m;
            bool sucessoDep1 = cc.Depositar(valorDeposito1);
            EscreverResultado("Depósito", valorDeposito1, sucessoDep1);
            cc.ConsultarSaldo(); 

            
            decimal valorDeposito2 = -10.00m;
            bool sucessoDep2 = cc.Depositar(valorDeposito2);
            EscreverResultado("Depósito (inválido)", valorDeposito2, sucessoDep2);

            
            decimal valorSaque1 = 150.00m;
            bool sucessoSaque1 = cc.Sacar(valorSaque1);
            EscreverResultado("Saque", valorSaque1, sucessoSaque1);
            cc.ConsultarSaldo(); 

            
            decimal valorSaque2 = 500.00m;
            bool sucessoSaque2 = cc.Sacar(valorSaque2);
            EscreverResultado("Saque (excede saldo)", valorSaque2, sucessoSaque2);
            cc.ConsultarSaldo(); 
            Console.WriteLine("Descrição geral da conta:");
            cc.ToString();
        }


        private static void TestarContaEspecial()
        {
            Console.WriteLine("\n## 2. Testando Conta Especial - Limite ##");
            decimal limite = 500.00m;
            ContaCorrente ce = new ContaEspecial("Maria Souza", "002-CE", limite);
            Console.WriteLine($"Conta: {ce.Titular} ({ce.NumeroConta})");
            ce.ConsultarSaldo(); 

            
            decimal valorDeposito = 100.00m;
            ce.Depositar(valorDeposito);
            EscreverResultado("Depósito", valorDeposito, true);
            ce.ConsultarSaldo();

            
            decimal valorSaque1 = 100.00m;
            bool sucessoSaque1 = ce.Sacar(valorSaque1);
            EscreverResultado("Saque (zera saldo)", valorSaque1, sucessoSaque1);
            ce.ConsultarSaldo();

            
            decimal valorSaque2 = 300.00m;
            bool sucessoSaque2 = ce.Sacar(valorSaque2);
            EscreverResultado("Saque (usa limite)", valorSaque2, sucessoSaque2);
            ce.ConsultarSaldo(); 

            
            decimal valorSaque3 = 300.00m; 
            bool sucessoSaque3 = ce.Sacar(valorSaque3);
            EscreverResultado("Saque (excede limite)", valorSaque3, sucessoSaque3);
            ce.ConsultarSaldo();
            Console.WriteLine("Descrição geral da conta:");
            ce.ToString();
        }


        private static void TestarContaPoupanca()
        {
            Console.WriteLine("\n## 3. Testando Conta Poupança - Rendimento ##");
            decimal taxa = 0.05m; 
            ContaCorrente cp = new ContaPoupanca("Pedro Rocha", "003-CP", taxa);
            Console.WriteLine($"Conta: {cp.Titular} ({cp.NumeroConta}) | Taxa de Rendimento: {taxa:P0}");
            cp.ConsultarSaldo(); 

            
            decimal valorDeposito = 1000.00m;
            cp.Depositar(valorDeposito);
            EscreverResultado("Depósito", valorDeposito, true);
            cp.ConsultarSaldo(); 

            
            Console.Write("  -> Sucesso: Aplicação de rendimento... ");
            cp.AplicarRendimento(); 
            Console.WriteLine("FEITO.");
            cp.ConsultarSaldo();

            
            decimal valorSaque = 50.00m;
            bool sucessoSaque1 = cp.Sacar(valorSaque);
            EscreverResultado("Saque", valorSaque, sucessoSaque1);
            cp.ConsultarSaldo(); 

            decimal valorSaque2 = 2000.00m;
            bool sucessoSaque2 = cp.Sacar(valorSaque2);
            EscreverResultado("Saque (excede saldo)", valorSaque2, sucessoSaque2);
            Console.WriteLine("Descrição geral da conta:");
            cp.ToString();
        }
    }
}