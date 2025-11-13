using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_3___POO_com_contas_bancarias
{
    public class ContaCorrente : ITransacionavel
    {
        protected decimal _saldo;
        public string Titular { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get => _saldo; }
        protected decimal TarifaManutencao { get; set; }

        public ContaCorrente(decimal tarifaManutencao = 30m)
        {
            this.Titular = string.Empty;
            this.NumeroConta = string.Empty;
            this._saldo = 0;
        }
        public ContaCorrente(string titular, string numeroConta, decimal tarifaManutencao = 30m)
        {
            this.Titular = titular;
            this.NumeroConta = numeroConta;
            this._saldo = 0;
        }
       
        public void ConsultarSaldo()
        {
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
            Console.WriteLine($"Saldo atual: {Saldo.ToString("C", culturaBrasileira)}.");
        }
        public virtual bool Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                return false;
            }
            _saldo += valor;
            return true;
        }
        public virtual bool Sacar(decimal valor)
        {
            if (valor <= 0 || valor > _saldo)
            {
                return false;
            }
            _saldo -= valor;
            return true;
        }

        public virtual decimal CalcularTarifaManutencao()
        {
            return TarifaManutencao;
        }
        public  override string ToString()
        {
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
            return $"ContaCorrente(Titular: {Titular}, NumeroConta: {NumeroConta}, Saldo: {Saldo.ToString("C", culturaBrasileira)}, Tarifa de manutenção: {TarifaManutencao.ToString("C", culturaBrasileira)}.)";
        }

        public virtual bool AplicarRendimento()
        {
            return false;
        }

    }
}
