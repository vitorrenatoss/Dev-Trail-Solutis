using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Atividade_4___coleções_e_LINQ
{
    public class Program
    {

        static void Main()
        {
            //TesteListas();
            //TesteDicionario();
            //TesteLinq1();
            //TesteLinq2();
            //StringTransacoes();
            StringTransacoes2();
        }


        static void TesteListas() //Exercício 1 - Slide Coleções LINQ
        {
            List<ContaCorrente> contas = new List<ContaCorrente>() { };

            contas.Add(new ContaCorrente(1, "Alice", "Corrente", 1500));
            contas.Add(new ContaCorrente(2, "Marcio", "Poupança", 800));
            contas.Add(new ContaCorrente(3, "Icaro", "Corrente", 1200));
            contas.Add(new ContaCorrente(4, "Diana", "Poupança", 500));
            contas.Add(new ContaCorrente(5, "Eva", "Corrente", 2000));

            Console.WriteLine("Todas as contas:");
            foreach (var conta in contas)
            {
                Console.WriteLine($"Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            }
            Console.WriteLine($"Total de contas: {contas.Count()}");

            Console.WriteLine("\nContas com saldo acima de 1000:");
            List<ContaCorrente> SaldoAcimaDe1000 = contas.FindAll(conta => conta.Saldo > 1000).ToList();
            foreach (var conta in SaldoAcimaDe1000)
            {
                Console.WriteLine($"Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            }

            Console.WriteLine("\nRemovendo conta de Alice:");
            contas.RemoveAll(conta => conta.Titular == "Alice");
            Console.WriteLine("Todas as contas:");
            foreach (var conta in contas)
            {
                Console.WriteLine($"Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            }
            Console.WriteLine($"Total de contas: {contas.Count()}");

            ContaCorrente[] contas2 = new ContaCorrente[3]
            {
                new ContaCorrente(6, "Francisco", "Corrente", 1100),
                new ContaCorrente(7, "Graça", "Poupança", 900),
                new ContaCorrente(8, "Henrique", "Corrente", 1300)
            };
            Console.WriteLine("\n novas contas do array estático:");
            for (int i = 0; i < contas2.Length; i++)
            {
                Console.WriteLine($"Conta: {contas2[i].NumeroConta}, Titular: {contas2[i].Titular}, Saldo: {contas2[i].Saldo}");
            }

            Console.WriteLine("\ntentando adicionar no vetor estático:");
            try
            {
                contas2[3] = new ContaCorrente(9, "Ivy", "Poupança", 700);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: Não é possível adicionar mais contas ao array estático. ");
            }
        }

        static void TesteDicionario() //Exercício 2 - Slide Coleções LINQ
        {
            List<ContaCorrente> contas = new List<ContaCorrente>() { };

            contas.Add(new ContaCorrente(1, "Alice", "Corrente", 1500));
            contas.Add(new ContaCorrente(2, "Marcio", "Poupança", 800));
            contas.Add(new ContaCorrente(3, "Icaro", "Corrente", 1200));
            contas.Add(new ContaCorrente(4, "Diana", "Poupança", 500));
            contas.Add(new ContaCorrente(5, "Eva", "Corrente", 2000));
            contas.Add(new ContaCorrente(6, "Francisco", "Corrente", -100));
            contas.Add(new ContaCorrente(7, "Graça", "Poupança", -50));

            Dictionary<int, ContaCorrente> dicionarioContas = new Dictionary<int, ContaCorrente>();
            foreach (var conta in contas)
            {
                if (!dicionarioContas.ContainsKey(conta.NumeroConta))
                {
                    dicionarioContas.Add(conta.NumeroConta, conta);

                }
            }
            Console.WriteLine("Contas no dicionário:");
            foreach (KeyValuePair<int, ContaCorrente> item in dicionarioContas)
            {
                Console.WriteLine($"Conta: {item.Key}, Titular: {item.Value.Titular}, Saldo: {item.Value.Saldo}");
            }

            Console.WriteLine("\nAcessando conta pelo número da conta (3):");
            Console.WriteLine($"Conta com nº3 existe?: {(dicionarioContas.ContainsKey(3) ? "Sim" : "Não")}");
            if (dicionarioContas.TryGetValue(3, out ContaCorrente conta3))
            {
                Console.WriteLine($"Conta: {conta3.NumeroConta}, Titular: {conta3.Titular}, Saldo: {conta3.Saldo}");
            }

            Console.WriteLine("\nRemovendo conta de nº2:");
            dicionarioContas.Remove(2);
            Console.WriteLine("Contas no dicionário:");
            foreach (KeyValuePair<int, ContaCorrente> item in dicionarioContas)
            {
                Console.WriteLine($"Conta: {item.Key}, Titular: {item.Value.Titular}, Saldo: {item.Value.Saldo}");
            }

            var hashContas = new HashSet<KeyValuePair<int, ContaCorrente>>(dicionarioContas);


        }

        static void TesteLinq1() //Exercício 3 - Slide Coleçoes e LINQ
        {
            List<ContaCorrente> contas = new List<ContaCorrente>() { };
            contas.Add(new ContaCorrente(1, "Alice", "Corrente", 1500));
            contas.Add(new ContaCorrente(2, "Marcio", "Poupança", 800));
            contas.Add(new ContaCorrente(3, "Icaro", "Corrente", 1200));
            contas.Add(new ContaCorrente(4, "Diana", "Poupança", 500));
            contas.Add(new ContaCorrente(5, "Eva", "Corrente", 2000));
            contas.Add(new ContaCorrente(6, "Francisco", "Corrente", -100));
            contas.Add(new ContaCorrente(7, "Graça", "Poupança", -50));

            Dictionary<int, ContaCorrente> dicionarioContas = new Dictionary<int, ContaCorrente>();
            foreach (var conta in contas)
            {
                if (!dicionarioContas.ContainsKey(conta.NumeroConta))
                {
                    dicionarioContas.Add(conta.NumeroConta, conta);

                }
            }

            Console.WriteLine("\n--------------Method syntax---------------------\n");
            var contasNegativasMetodo = dicionarioContas.Values.Where(conta => conta.Saldo < 0);
            Console.WriteLine("\nContas com saldo negativo:");
            foreach (var conta in contasNegativasMetodo)
            {
                Console.WriteLine($"Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            }
            Console.WriteLine($"Total de contas negativas: {contasNegativasMetodo.Count()}");


            Console.WriteLine("\n--------------Query syntax---------------------\n");

            var contasNegativas = from conta in dicionarioContas.Values
                                  where conta.Saldo < 0
                                  select conta;
            Console.WriteLine("Contas com saldo negativo:\n");
            contasNegativas.ToList().ForEach(conta =>
            {
                Console.WriteLine($"Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            });
            Console.WriteLine($"\nTotal de contas negativas: {contasNegativas.Count()}");
        }

        static void TesteLinq2() //Exercício 4 - Slide Coleçoes e LINQ
        {
            List<ContaCorrente> contas = new List<ContaCorrente>() { };
            contas.Add(new ContaCorrente(1, "Alice", "Corrente", 1500));
            contas.Add(new ContaCorrente(2, "Marcio", "Poupança", 800));
            contas.Add(new ContaCorrente(3, "Icaro", "Corrente", 1200));
            contas.Add(new ContaCorrente(4, "Diana", "Poupança", 500));
            contas.Add(new ContaCorrente(5, "Eva", "Corrente", 2000));
            contas.Add(new ContaCorrente(6, "Francisco", "Corrente", -100));
            contas.Add(new ContaCorrente(7, "Graça", "Poupança", -50));

            Dictionary<int, ContaCorrente> dicionarioContas = new Dictionary<int, ContaCorrente>();
            foreach (var conta in contas)
            {
                if (!dicionarioContas.ContainsKey(conta.NumeroConta))
                {
                    dicionarioContas.Add(conta.NumeroConta, conta);

                }
            }


            // 0. testando busca com query syntax
            var buscarC1 = (from conta in dicionarioContas.Values
                            where conta.NumeroConta == 1
                            select conta).FirstOrDefault();
            Console.WriteLine($"\nBusca pela conta nº1 (Query Syntax):Conta nº {buscarC1.NumeroConta}, titular: {buscarC1.Titular}");

            //1. Projetando tipo anônimo (Titular e saldo) com method syntax
            var projecaoTitularSaldo = dicionarioContas.Values.Select(conta => new
            {
                conta.Titular,
                conta.Saldo
            });
            Console.WriteLine("\nProjeção de Titular e Saldo:");
            foreach (var item in projecaoTitularSaldo)
            {
                Console.WriteLine($"Titular: {item.Titular}, Saldo: {item.Saldo}");
            }
            ;

            //2. Agrupando contas por tipo
            var agrupamentoPorTipo = dicionarioContas.Values.GroupBy(conta => conta.TipoConta);
            Console.WriteLine("\nAgrupamento por tipo de conta:");
            foreach (var grupo in agrupamentoPorTipo)
            {
                Console.WriteLine($"\nTipo de Conta: {grupo.Key}\n");
                foreach (var conta in grupo)
                {
                    Console.WriteLine($"  Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
                }
                Console.WriteLine();
            }

            //3. Ordenando contas por saldo decrescente e titular 
            var contasOrdenadas = dicionarioContas.Values
                                        .OrderByDescending(conta => conta.Saldo)
                                        .ThenBy(conta => conta.Titular);
            Console.WriteLine("Contas ordenadas por saldo (decrescente) e titular:\n");
            foreach (var conta in contasOrdenadas)
            {
                Console.WriteLine($"Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            }
            ;





            //4. Agrupamento, ordenação e projeção juntos
            Console.WriteLine("\nCombinando todas as operações:");
            var AllAtOnce = from conta in dicionarioContas.Values
                            group conta by conta.TipoConta into grupo
                            select new
                            {
                                TipoConta = grupo.Key,
                                Contas = grupo.OrderByDescending(c => c.Saldo)
                                            .ThenBy(c => c.Titular)
                                            .ToList(),
                                TotalContas = grupo.Count(),
                                projecaoTitularSaldo = grupo.Select(c => new
                                {
                                    c.Titular,
                                    c.Saldo
                                }).ToList()
                            };

            Console.WriteLine("\n*******Resumo por tipo de conta:\n");
            foreach (var grupo in AllAtOnce)
            {
                Console.WriteLine($"*******lista de contas do tipo {grupo.TipoConta} (total: {grupo.TotalContas}):");
                foreach (var conta in grupo.Contas)
                {
                    Console.WriteLine($"  Conta: {conta.NumeroConta}, Titular: {conta.Titular}, Saldo: {conta.Saldo}");
                }
                Console.WriteLine($"\n*******Testando projeção de tipo anônimo (Titular e Saldo):");
                foreach (var item in grupo.projecaoTitularSaldo)
                {
                    Console.WriteLine($"Titular: {item.Titular}, Saldo: {item.Saldo}");
                }
                ;
                Console.WriteLine();
            }

        }

        static void StringTransacoes() //Exercício 5 - Slide Coleçoes e LINQ usando as classes
        {
            ContaCorrente conta1 = new ContaCorrente(1, "Alice Matos", "Corrente", 0);
            conta1.AdicionarTransacao(1, "01/03/2022 10:00", "Credito", 1500, "Depósito inicial");
            conta1.AdicionarTransacao(1, "24/03/2022 08:30", "Debito", 500, "Aluguel");
            conta1.AdicionarTransacao(1, "23/03/2022 09:15", "Credito", 1000, "Salário");
            conta1.AdicionarTransacao(1, "25/03/2022 14:00", "Debito", 200, "Supermercado");
            conta1.AdicionarTransacao(1, "26/03/2022 16:45", "Debito", 150, "Salão de Beleza");

            conta1.ExibirExtrato();

        }

        static void StringTransacoes2() //Exercício 5 - Slide Coleçoes e LINQ (foco no LINQ e filtro)
        {
            List<Transacao> transacoes = new List<Transacao>
            {
                new Transacao(1001, "22/10/2025 14:25", "Credito",500, "Depósito inicial"),
                new Transacao(1002, "21/10/2025 09:00", "Debito", 120, "Compra mercado"),
                new Transacao(1003, "23/10/2025 11:30", "Credito", 900, "Transferência recebida"),
                new Transacao(1001, "24/10/2025 08:45", "Debito",1300, "Pagamento escola"),
                new Transacao(1002, "25/10/2025 10:15", "Credito", 300, "Depósito"),
                new Transacao(1003, "26/10/2025 12:00", "Debito", 35, "Assinatura streaming"),
                new Transacao(1001, "27/10/2025 16:00", "Credito", 2500, "Salário")
            };


            var TransacoesAgrupadas = from transacao in transacoes
                                      group transacao by transacao.NumeroConta into grupo
                                      select new
                                      {
                                          NumeroConta = grupo.Key,
                                          Transacoes = grupo.OrderByDescending(t => t.Data).ToList(),
                                          TotalTransacoes = grupo.Count(),
                                          SaldoFinal = grupo.Sum(t => t.Tipo == "Credito" ? t.Valor : -t.Valor)
                                      };

            foreach (var grupo in TransacoesAgrupadas)
            {
                Console.WriteLine($"\nExtrato da Conta {grupo.NumeroConta:D3}:\n{new string('*', 40)}");
                foreach (var transacao in grupo.Transacoes)
                {
                    Console.WriteLine($"{transacao.Data:dd/MM/yyyy HH:mm} | {transacao.Tipo} | {transacao.Valor.ToString("C", new CultureInfo("pt-BR"))} | {transacao.Descricao} |");
                }
                Console.WriteLine($"Saldo final: {grupo.SaldoFinal.ToString("C", new CultureInfo("pt-BR"))}");
                Console.WriteLine();

            }

        }
    }
}