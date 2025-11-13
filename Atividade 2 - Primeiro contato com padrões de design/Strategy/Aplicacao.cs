using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Atividade_2___Primeiro_contato_com_padrões_de_design.Strategy
{
    internal class Aplicacao
    {
        private Pessoa usuario = new();
        static void Main()
        {
            Aplicacao app = new();
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
            Console.WriteLine("=== Sistema de Pagamento ===");
            app.MensagemBoasVindas();
            app.ValidarEntrada();
        }
        void MensagemBoasVindas()
        {
            Console.WriteLine("Bem-vindo ao sistema de pagamento!");
            Console.WriteLine("1. Digite 'cartao' para pagamento com cartão.\n 2. Digite 'dinheiro' para pagamento com dinheo.\n3. Digite 'pix' para pagamento via PIX.");
        }
        void ValidarEntrada()
        {
            while (true)
            {
                bool erro = false;
                double valorConvertido = 0; 
                Console.Write("Insira o método de pagamento e o valor  (use vírgula para casa decimal): ");
                string[] entrada = Console.ReadLine().Split();
                string metodo = entrada[0].ToLower();
                string valor = entrada.Length > 1 ? entrada[1] : "";
                if (string.IsNullOrEmpty(metodo) || string.IsNullOrWhiteSpace(metodo) || string.IsNullOrEmpty(valor) || string.IsNullOrWhiteSpace(valor))
                {
                    erro = true;
                    Console.Write("Erro: método ou valor inválido\n");
                }
                if (metodo != "cartao" && metodo != "dinheiro" && metodo != "pix")
                {
                    erro = true;
                    Console.Write("Erro: método ou valor inválido\n");
                }
                if (!erro && !double.TryParse(valor, NumberStyles.Number, CultureInfo.CurrentCulture, out valorConvertido) && valorConvertido <=0)
                {
                    erro = true;
                    Console.Write("Erro: método ou valor inválido\n");
                }

                if (!erro)
                {
                    ExecutarPagamento(metodo, valorConvertido);
                    break;
                }
            }

            void ExecutarPagamento(string metodo, double valor)
            {
                switch (metodo)
                {
                    case "cartao":
                        usuario.SetMetodoPagamento(new PagamentoCartao());
                        usuario.Pagar(metodo, valor);
                        break;
                    case "dinheiro":
                        usuario.SetMetodoPagamento(new PagamentoDinheiro());
                        usuario.Pagar(metodo, valor);
                        break;
                    case "pix":
                        usuario.SetMetodoPagamento(new PagamentoPix());
                        usuario.Pagar(metodo, valor);
                        break;
                    default:
                        Console.WriteLine("Método de pagamento inválido. Tente novamente.");
                        break;
                }
                Console.Write("Deseja realizar outro pagamento?\nDigite 's' para sim ou 'n' para não: ");
                string resposta = Console.ReadLine().ToLower();
                if (resposta == "s")
                {
                    Console.WriteLine();
                    ValidarEntrada();
                }
                else
                {
                    Console.WriteLine("Obrigado por utilizar o sistema de pagamento. Até mais!");
                }
            }
        }
    }
}
