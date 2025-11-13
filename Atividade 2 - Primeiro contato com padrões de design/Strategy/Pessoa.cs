using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_2___Primeiro_contato_com_padrões_de_design.Strategy
{
    internal class Pessoa
    {
        private IPagamento metodoPagamento;
        public Pessoa()
        {
            
        }
        public void SetMetodoPagamento(IPagamento metodoPagamento)
        {
            this.metodoPagamento = metodoPagamento;
        }
        public void Pagar(string metodo, double valor)
        {
            metodoPagamento.RealizarPagamento(metodo, valor);
        }
    }
}
    
