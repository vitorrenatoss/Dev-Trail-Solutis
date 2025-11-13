using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_4___coleções_e_LINQ
{
    internal class Transacao
    {
        public Guid Id { get; set; }
        public int NumeroConta { get; set; }
        public string Tipo { get; set; } // "Credito" ou "Debito"
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }


        public Transacao(int numeroConta, string data, string tipo, decimal valor, string descricao)
        {
            Id = Guid.NewGuid();
            NumeroConta = numeroConta;
            Tipo = tipo;
            Descricao = descricao;
            Valor = valor;
            Data = DateTime.ParseExact(data, "dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"));

        }
    }
}
