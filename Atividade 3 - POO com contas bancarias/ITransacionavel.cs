using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_3___POO_com_contas_bancarias
{
    internal interface ITransacionavel
    {
        bool Depositar(decimal valor);
        bool Sacar(decimal valor);

    }
}
