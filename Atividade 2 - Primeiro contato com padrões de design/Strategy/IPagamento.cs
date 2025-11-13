using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_2___Primeiro_contato_com_padrões_de_design.Strategy
{
    public interface IPagamento
    {
        void RealizarPagamento(string metodo, double valor);
    }
}
