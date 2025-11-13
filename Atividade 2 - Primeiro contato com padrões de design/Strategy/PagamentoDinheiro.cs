using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_2___Primeiro_contato_com_padrões_de_design.Strategy
{
    internal class PagamentoDinheiro : IPagamento
    {
        public void RealizarPagamento(string metodo, double valor)
        {
            Console.WriteLine($"Pagamento de R$ {valor:F2} realizado em dinheiro.");
        }
    }
    
    }

